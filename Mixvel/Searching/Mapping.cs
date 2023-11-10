using AutoMapper;

namespace Mixvel.Searching;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<SearchResponse, Core.Searching.SearchResponse>();
        CreateMap<Route, Core.Searching.Route>();
        
        CreateMap<SearchFilters, Core.Searching.SearchFilters>();
        CreateMap<SearchRequest, Core.Searching.SearchRequest>();
    }
}