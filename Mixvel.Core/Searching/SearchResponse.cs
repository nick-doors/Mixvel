namespace Mixvel.Core.Searching;

public class SearchResponse
{
    public static SearchResponse Empty = new SearchResponse();

    public SearchResponse(params Route[] routes)
    {
        Routes = routes;
        MinPrice = routes.Min(x => x.Price);
        MaxPrice = routes.Max(x => x.Price);
        MinMinutesRoute = routes.Min(x => (x.DestinationDateTime - x.OriginDateTime).Minutes);
        MaxMinutesRoute = routes.Max(x => (x.DestinationDateTime - x.OriginDateTime).Minutes);
    }

    // Mandatory
    // Array of routes
    public Route[] Routes { get; private set; }

    // Mandatory
    // The cheapest route
    public decimal MinPrice { get; private set; }

    // Mandatory
    // Most expensive route
    public decimal MaxPrice { get; private set; }

    // Mandatory
    // The fastest route
    public int MinMinutesRoute { get; private set; }

    // Mandatory
    // The longest route
    public int MaxMinutesRoute { get; private set; }
}