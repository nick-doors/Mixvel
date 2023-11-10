namespace Mixvel.Core.Searching;

public sealed record Route
{
    // Mandatory
    // Identifier of the whole route
    public Guid Id { get; } = Guid.NewGuid();

    // Mandatory
    // Start point of route
    public required string Origin { get; init; }

    // Mandatory
    // End point of route
    public required string Destination { get; init; }

    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; init; }

    // Mandatory
    // End date of route
    public DateTime DestinationDateTime { get; init; }

    // Mandatory
    // Price of route
    public decimal Price { get; init; }

    // Mandatory
    // TimeLimit. After it expires, route became not actual
    public DateTime TimeLimit { get; init; }
}