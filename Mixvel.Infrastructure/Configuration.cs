using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mixvel.Core.Searching;
using Mixvel.Infrastructure.Options;
using Mixvel.Infrastructure.Searching.ProviderOne;
using Mixvel.Infrastructure.Searching.ProviderTwo;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Mixvel.Infrastructure;

public static class Configuration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .RetryAsync(3);

        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

        services.AddAutoMapper(c => c.AddMaps(typeof(Configuration).Assembly));

        services
            .AddHttpClient<ProviderOneApi>((sp, c) =>
            {
                var options = sp.GetRequiredService<IOptions<ProviderOneOptions>>();
                c.BaseAddress = options.Value.Uri;
            })
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(timeoutPolicy);

        services
            .AddHttpClient<ProviderTwoApi>((sp, c) =>
            {
                var options = sp.GetRequiredService<IOptions<ProviderTwoOptions>>();
                c.BaseAddress = options.Value.Uri;
            })
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(timeoutPolicy);

        services
            .Scan(c => c
                .FromAssemblies(typeof(Configuration).Assembly)
                .AddClasses(classes => classes.AssignableTo<ISearchService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
    }
}