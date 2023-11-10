namespace Mixvel.Infrastructure.Options;

public abstract record ProviderOptions
{
    public required Uri Uri { get; init; }
}