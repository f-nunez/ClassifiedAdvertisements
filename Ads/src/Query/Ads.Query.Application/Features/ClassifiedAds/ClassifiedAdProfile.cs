using Ads.Query.Domain.Entities;
using AutoMapper;

namespace Ads.Query.Application.Features.ClassifiedAds;

public class ClassifiedAdProfile : Profile
{
    public ClassifiedAdProfile()
    {
        CreateMap<ClassifiedAd, ClassifiedAdDto>()
            .ForMember(
                d => d.CreatedBy,
                m => m.MapFrom(s => s.CreatedBy)
            ).ForMember(
                d => d.CreatedOn,
                m => m.MapFrom(s => s.CreatedOn)
            ).ForMember(
                d => d.Description,
                m => m.MapFrom(s => s.Description)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.PublishedBy,
                m => m.MapFrom(s => s.PublishedBy)
            ).ForMember(
                d => d.PublishedOn,
                m => m.MapFrom(s => s.PublishedOn)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.MapFrom(s => s.UpdatedBy)
            ).ForMember(
                d => d.UpdatedOn,
                m => m.MapFrom(s => s.UpdatedOn)
            ).ForMember(
                d => d.Version,
                m => m.MapFrom(s => s.Version)
            );

        CreateMap<ClassifiedAd, ClassifiedAdPaginatedListItemDto>()
            .ForMember(
                d => d.Description,
                m => m.MapFrom(s => s.Description)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.PublishedBy,
                m => m.MapFrom(s => s.PublishedBy)
            ).ForMember(
                d => d.PublishedOn,
                m => m.MapFrom(s => s.PublishedOn)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            );
    }
}