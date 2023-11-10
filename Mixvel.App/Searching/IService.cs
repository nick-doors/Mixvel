using Mixvel.Core.Searching;

namespace Mixvel.App.Searching;

public interface IService
{
    Task<SearchResponse> SearchAsync(
        SearchRequest request,
        CancellationToken cancellationToken);
}