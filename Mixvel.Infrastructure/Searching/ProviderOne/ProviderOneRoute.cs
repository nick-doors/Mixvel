namespace Mixvel.Infrastructure.Searching.ProviderOne;

public sealed record ProviderOneRoute
{
    // Mandatory
    // Start point of route
    public required string From { get; set; }

    // Mandatory
    // End point of route
    public required string To { get; set; }

    // Mandatory
    // Start date of route
    public DateTime DateFrom { get; set; }

    // Mandatory
    // End date of route
    public DateTime DateTo { get; set; }

    // Mandatory
    // Price of route
    public decimal Price { get; set; }

    // Mandatory
    // TimeLimit. After it expires, route became not actual
    public DateTime TimeLimit { get; set; }
}