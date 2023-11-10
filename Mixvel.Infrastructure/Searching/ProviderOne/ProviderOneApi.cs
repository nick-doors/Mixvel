using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Mixvel.Core.Searching;
using Newtonsoft.Json;

namespace Mixvel.Infrastructure.Searching.ProviderOne;

internal sealed class ProviderOneApi : ISearchService
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProviderOneApi> _logger;
    private readonly HttpClient _httpClient;

    public ProviderOneApi(
        HttpClient httpClient,
        IMapper mapper,
        ILogger<ProviderOneApi> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<SearchResponse> SearchAsync(
        SearchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var postRequest = _mapper.Map<ProviderOneSearchRequest>(request);
            var postContent = new StringContent(
                JsonConvert.SerializeObject(postRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                "/api/v1/search",
                postContent,
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return SearchResponse.Empty;

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            var data = JsonConvert.DeserializeObject<ProviderOneSearchResponse>(content);

            if (data == null)
                return SearchResponse.Empty;

            var routes = _mapper.Map<Route[]>(data.Routes);

            return new SearchResponse(routes);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while searching API {ApiType}", nameof(ProviderOneApi));
            return SearchResponse.Empty;
        }
    }

    public async Task<bool> IsAvailableAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/v1/ping",
                cancellationToken);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while ping API {ApiType}", nameof(ProviderOneApi));
            return false;
        }
    }

    public string Id => GetType().FullName!;
}