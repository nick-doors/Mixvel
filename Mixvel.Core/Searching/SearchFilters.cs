namespace Mixvel.Core.Searching;

public sealed record SearchFilters
{
    // Optional
    // End date of route
    public DateTime? DestinationDateTime { get; init; }
    
    // Optional
    // Maximum price of route
    public decimal? MaxPrice { get; init; }
    
    // Optional
    // Minimum value of time limit for route
    public DateTime? MinTimeLimit { get; init; }
    
    // Optional
    // Forcibly search in cached data
    public bool? OnlyCached { get; init; }
}