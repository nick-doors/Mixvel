using AutoMapper;
using Mixvel.Core.Searching;

namespace Mixvel.Infrastructure.Searching.ProviderTwo;

internal class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<ProviderTwoRoute, Route>()
            .ForMember(
                x => x.Id,
                x => x.Ignore())
            .ForMember(
                x => x.Origin,
                x => x.MapFrom((z => z.Departure.Point)))
            .ForMember(
                x => x.Destination,
                x => x.MapFrom(z => z.Arrival.Point))
            .ForMember(
                x => x.OriginDateTime,
                x => x.MapFrom(z => z.Departure.Date))
            .ForMember(
                x => x.DestinationDateTime,
                x => x.MapFrom(z => z.Arrival.Date));

        CreateMap<SearchRequest, ProviderTwoSearchRequest>()
            .ForMember(
                x => x.Departure,
                x => x.MapFrom(z => z.Origin))
            .ForMember(
                x => x.Arrival,
                x => x.MapFrom(z => z.Destination))
            .ForMember(
                x => x.DepartureDate,
                x => x.MapFrom(z => z.Filters!.DestinationDateTime))
            .ForMember(
                x => x.MinTimeLimit,
                x => x.MapFrom(z => z.Filters!.MinTimeLimit));
    }
}