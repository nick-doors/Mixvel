using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Mixvel.Core.Searching;

namespace Mixvel.App.Searching.Factory;

internal sealed class SearchServiceFactory : ISearchServiceFactory
{
    private readonly IEnumerable<ISearchService> _services;
    private readonly ILogger<SearchServiceFactory> _logger;
    private static readonly ConcurrentDictionary<string, bool> Cache = new();

    public SearchServiceFactory(
        IEnumerable<ISearchService> services,
        ILogger<SearchServiceFactory> logger)
    {
        _services = services;
        _logger = logger;
    }

    public async Task RefreshStateAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var tasks = _services
                .Select(x => IsAvailableAsync(x, cancellationToken))
                .ToArray();

            await Task.WhenAll(tasks).WaitAsync(cancellationToken);

            foreach (var task in tasks)
            {
                Cache[task.Result.ServiceId] = task.Result.Available;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "Unexpected error while executing {Operation}",
                nameof(RefreshStateAsync));
            throw;
        }
    }

    public IEnumerable<ISearchService> GetServices()
    {
        return _services
            .Where(x => Cache
                .GetValueOrDefault(x.Id, true));
    }

    private static async Task<(string ServiceId, bool Available)> IsAvailableAsync(
        ISearchService service,
        CancellationToken cancellationToken)
    {
        var result = await service.IsAvailableAsync(cancellationToken);
        return (service.Id, result);
    }
}