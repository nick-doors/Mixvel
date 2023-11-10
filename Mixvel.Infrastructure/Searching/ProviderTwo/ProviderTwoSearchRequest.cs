namespace Mixvel.Infrastructure.Searching.ProviderTwo;

internal record ProviderTwoSearchRequest
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public required string Departure { get; set; }
    
    // Mandatory
    // End point of route, e.g. Sochi
    public required string Arrival { get; set; }
    
    // Mandatory
    // Start date of route
    public DateTime DepartureDate { get; set; }
    
    // Optional
    // Minimum value of time limit for route
    public DateTime? MinTimeLimit { get; set; }
}