namespace Mixvel.Core.Searching;

public interface ISearchService
{
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);

    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
    
    // Не было времени придумать что-то лучше, уже ночь, а к утру дедлайн ;)
    // Нужно для кеширования состояний
    string Id { get; }
}