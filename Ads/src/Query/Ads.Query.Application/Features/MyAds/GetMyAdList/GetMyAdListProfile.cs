using Ads.Query.Domain.Entities;
using AutoMapper;

namespace Ads.Query.Application.Features.MyAds.GetMyAdList;

public class GetMyAdListProfile : Profile
{
    public GetMyAdListProfile()
    {
        CreateMap<ClassifiedAd, GetMyAdListItemDto>()
            .ForMember(
                d => d.Description,
                m => m.MapFrom(s => s.Description)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.PublishedOn,
                m => m.MapFrom(s => s.PublishedOn)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            ).ForMember(
                d => d.UpdatedOn,
                m => m.MapFrom(s => s.UpdatedOn)
            ).ForMember(
                d => d.Version,
                m => m.MapFrom(s => s.Version)
            );
    }
}