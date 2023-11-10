namespace Mixvel.Infrastructure.Searching.ProviderTwo;

internal sealed record ProviderTwoRoute
{
    // Mandatory
    // Start point of route
    public required ProviderTwoPoint Departure { get; set; }
    
    // Mandatory
    // End point of route
    public required ProviderTwoPoint Arrival { get; set; }
    
    // Mandatory
    // Price of route
    public decimal Price { get; set; }
    
    // Mandatory
    // TimeLimit. After it expires, route became not actual
    public DateTime TimeLimit { get; set; }
}