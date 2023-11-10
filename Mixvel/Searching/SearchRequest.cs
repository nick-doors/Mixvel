namespace Mixvel.Searching;

public sealed record SearchRequest
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public required string Origin { get; init; }
    
    // Mandatory
    // End point of route, e.g. Sochi
    public required string Destination { get; init; }
    
    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; init; }
    
    // Optional
    public SearchFilters? Filters { get; init; }
}