using AutoMapper;
using Mixvel.Core.Searching;

namespace Mixvel.Infrastructure.Searching.ProviderOne;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<ProviderOneRoute, Route>()
            .ForMember(
                x => x.Id,
                x => x.Ignore())
            .ForMember(
                x => x.Origin,
                x => x.MapFrom((z => z.From)))
            .ForMember(
                x => x.Destination,
                x => x.MapFrom(z => z.To))
            .ForMember(
                x => x.OriginDateTime,
                x => x.MapFrom(z => z.DateFrom))
            .ForMember(
                x => x.DestinationDateTime,
                x => x.MapFrom(z => z.DateTo));

        CreateMap<SearchRequest, ProviderOneSearchRequest>()
            .ForMember(
                x => x.From,
                x => x.MapFrom(z => z.Origin))
            .ForMember(
                x => x.To,
                x => x.MapFrom(z => z.Destination))
            .ForMember(
                x => x.DateFrom,
                x => x.MapFrom(z => z.OriginDateTime))
            .ForMember(
                x => x.DateTo,
                x => x.MapFrom(z => z.Filters!.DestinationDateTime))
            .ForMember(
                x => x.MaxPrice,
                x => x.MapFrom(z => z.Filters!.MaxPrice));
    }
}