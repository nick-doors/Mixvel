using Mixvel.Infrastructure.Options;

namespace Mixvel;

public static class Configuration
{
    public static void AddUI(this IServiceCollection services)
    {
        services.AddAutoMapper(c =>
            c.AddMaps(typeof(Infrastructure.Configuration).Assembly));
        services.AddOptions<ProviderOneOptions>();
        services.AddOptions<ProviderTwoOptions>();
    }
}