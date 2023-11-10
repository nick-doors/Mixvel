namespace Mixvel.Searching;

public sealed record SearchResponse
{
    // Mandatory
    // Array of routes
    public required Route[] Routes { get; init; }

    // Mandatory
    // The cheapest route
    public required decimal MinPrice { get; init; }

    // Mandatory
    // Most expensive route
    public required decimal MaxPrice { get; init; }

    // Mandatory
    // The fastest route
    public required int MinMinutesRoute { get; init; }

    // Mandatory
    // The longest route
    public required int MaxMinutesRoute { get; init; }
}