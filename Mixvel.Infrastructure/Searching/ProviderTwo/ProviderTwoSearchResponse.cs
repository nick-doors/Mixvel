namespace Mixvel.Infrastructure.Searching.ProviderTwo;

internal record ProviderTwoSearchResponse
{
    // Mandatory
    // Array of routes
    public required ProviderTwoRoute[] Routes { get; set; }
}