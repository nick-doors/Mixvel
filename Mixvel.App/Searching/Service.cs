using Mixvel.App.Searching.Factory;
using Mixvel.Core.Searching;

namespace Mixvel.App.Searching;

internal sealed class Service : IService
{
    private readonly IEnumerable<ISearchService> _services;

    public Service(ISearchServiceFactory factory)
    {
        _services = factory.GetServices();
    }

    public async Task<SearchResponse> SearchAsync(
        SearchRequest request,
        CancellationToken cancellationToken)
    {
        var tasks = _services
            .Select(x => x.SearchAsync(request, cancellationToken))
            .ToArray();

        await Task.WhenAll(tasks).WaitAsync(cancellationToken);

        var routes = tasks
            .Select(x => x.Result)
            .SelectMany(x => x.Routes);

        routes = FilterRoutes(routes, request.Filters);

        return new SearchResponse(routes
            .ToArray());
    }

    private IEnumerable<Route> FilterRoutes(
        IEnumerable<Route> routes,
        SearchFilters? filters)
    {
        // Убираем "просроченные" маршруты
        routes = routes
            .Where(x => x.TimeLimit <= DateTime.UtcNow);

        if (filters == null)
            return routes;

        if (filters.DestinationDateTime != null)
        {
            routes = routes
                .Where(x => x.DestinationDateTime <= filters.DestinationDateTime.Value);
        }

        if (filters.MaxPrice != null)
        {
            routes = routes
                .Where(x => x.Price <= filters.MaxPrice.Value);
        }

        if (filters.MinTimeLimit != null)
        {
            routes = routes
                .Where(x => x.TimeLimit <= filters.MinTimeLimit.Value);
        }

        return routes;
    }
}