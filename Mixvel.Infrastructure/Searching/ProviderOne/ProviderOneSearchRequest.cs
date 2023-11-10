namespace Mixvel.Infrastructure.Searching.ProviderOne;

public sealed record ProviderOneSearchRequest
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public required string From { get; set; }

    // Mandatory
    // End point of route, e.g. Sochi
    public required string To { get; set; }

    // Mandatory
    // Start date of route
    public DateTime DateFrom { get; set; }

    // Optional
    // End date of route
    public DateTime? DateTo { get; set; }

    // Optional
    // Maximum price of route
    public decimal? MaxPrice { get; set; }
}