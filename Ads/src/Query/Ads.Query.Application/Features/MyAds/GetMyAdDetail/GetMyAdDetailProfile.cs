using Ads.Query.Domain.Entities;
using AutoMapper;

namespace Ads.Query.Application.Features.MyAds.GetMyAdDetail;

public class GetMyAdDetailProfile : Profile
{
    public GetMyAdDetailProfile()
    {
        CreateMap<ClassifiedAd, GetMyAdDetailDto>()
            .ForMember(
                d => d.Description,
                m => m.MapFrom(s => s.Description)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            ).ForMember(
                d => d.Version,
                m => m.MapFrom(s => s.Version)
            );
    }
}