using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Mixvel.Core.Searching;
using Newtonsoft.Json;

namespace Mixvel.App.Searching.Cache;

internal sealed class CachedSearchService : ISearchService
{
    private readonly IDistributedCache _cache;
    private readonly ISearchService _service;
    private readonly ILogger<CachedSearchService> _logger;

    public CachedSearchService(
        IDistributedCache cache,
        ISearchService service,
        ILogger<CachedSearchService> logger)
    {
        _cache = cache;
        _service = service;
        _logger = logger;
    }

    public async Task<SearchResponse> SearchAsync(
        SearchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var key = GetKey(request);

            if (request.Filters?.OnlyCached == true)
            {
                var cachedResponse = await _cache.GetStringAsync(key, cancellationToken);
                return cachedResponse == null
                    ? SearchResponse.Empty
                    : JsonConvert.DeserializeObject<SearchResponse>(cachedResponse)!;
            }

            var response = await _service.SearchAsync(
                request,
                cancellationToken);

            await SaveAsync(key, response, cancellationToken);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                "Error while using cached API {ApiType}",
                this.GetType().Name);
            return SearchResponse.Empty;
        }
    }

    public Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
    {
        return _service.IsAvailableAsync(cancellationToken);
    }

    public string Id => _service.Id;

    private string GetKey(SearchRequest request)
    {
        var k = new Dictionary<string, string?>
        {
            ["s"] = GetType().FullName,
            ["o"] = request.Origin,
            ["d"] = request.Destination,
            ["od"] = request.OriginDateTime.ToString("yyyyMMdd"),
            ["dd"] = request.Filters?.DestinationDateTime?.ToString("yyyyMMdd"),
            ["mt"] = request.Filters?.MinTimeLimit?.ToString("yyyyMMdd"),
            ["p"] = request.Filters?.MaxPrice?.ToString()
        };

        return string.Join("_", k
            .Select(x => $"{x.Key}_{x.Value}"));
    }

    private async Task SaveAsync(
        string key,
        SearchResponse response,
        CancellationToken cancellationToken)
    {
        await Task
            .WhenAll(
                // В ТЗ указано, что нужно хранить маршруты в разрезе идентификатора в кеше
                SaveRoutesAsync(response.Routes, cancellationToken),

                // В теории можно хранить только список идентификаторов маршрутов в кеше и затем сначала
                // получать этот списон, а вторым запросом маршруты, да, так дороже, но быстрее
                SaveResponseAsync(key, response, cancellationToken))
            .WaitAsync(cancellationToken);
    }

    private async Task SaveRoutesAsync(
        Route[] routes,
        CancellationToken cancellationToken)
    {
        if (!routes.Any())
            return;

        var tasks = routes
            .Select(x => _cache.SetStringAsync(
                x.Id.ToString(),
                JsonConvert.SerializeObject(x),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = x.TimeLimit,
                },
                cancellationToken))
            .ToArray();

        await Task.WhenAll(tasks).WaitAsync(cancellationToken);
    }

    private async Task SaveResponseAsync(
        string key,
        SearchResponse response,
        CancellationToken cancellationToken)
    {
        await _cache.SetStringAsync(
            key,
            JsonConvert.SerializeObject(response),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = response.Routes
                    .Max(z => z.TimeLimit)
            },
            cancellationToken);
    }
}