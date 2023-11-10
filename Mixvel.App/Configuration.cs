using Microsoft.Extensions.DependencyInjection;
using Mixvel.App.Searching;
using Mixvel.App.Searching.Cache;
using Mixvel.App.Searching.Factory;
using Mixvel.Core.Searching;

namespace Mixvel.App;

public static class Configuration
{
    public static void AddApp(this IServiceCollection services)
    {
        // Тут мы можем использовать любую реализацию распредленного кеша, например Redis
        services.AddDistributedMemoryCache();
        services.Decorate<ISearchService, CachedSearchService>();
        services.AddScoped<ISearchServiceFactory, SearchServiceFactory>();
        services.AddScoped<IService, Service>();
        services.AddHostedService<SearchServiceStateBackgroundService>();
    }
}