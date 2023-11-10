namespace Mixvel.Searching;

public sealed record Route
{
    // Mandatory
    // Identifier of the whole route
    public required Guid Id { get; init; }

    // Mandatory
    // Start point of route
    public required string Origin { get; init; }

    // Mandatory
    // End point of route
    public required string Destination { get; init; }

    // Mandatory
    // Start date of route
    public required DateTime OriginDateTime { get; init; }

    // Mandatory
    // End date of route
    public required DateTime DestinationDateTime { get; init; }

    // Mandatory
    // Price of route
    public required decimal Price { get; init; }

    // Mandatory
    // TimeLimit. After it expires, route became not actual
    public required DateTime TimeLimit { get; init; }
}