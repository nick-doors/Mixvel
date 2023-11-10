namespace Mixvel.Infrastructure.Searching.ProviderOne;

public sealed record ProviderOneSearchResponse
{
    // Mandatory
    // Array of routes
    public required ProviderOneRoute[] Routes { get; set; }
}