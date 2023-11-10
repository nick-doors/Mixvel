using Mixvel.Core.Searching;

namespace Mixvel.App.Searching.Factory;

internal interface ISearchServiceFactory
{
    Task RefreshStateAsync(
        CancellationToken cancellationToken);

    IEnumerable<ISearchService> GetServices();
}