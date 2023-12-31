using AutoMapper;
using Users.Domain.Entities;

namespace Users.Application.Features.UserProfiles.Queries.GetUserProfileList;

public class GetUserProfileListProfile : Profile
{
    public GetUserProfileListProfile()
    {
        CreateMap<User, GetUserProfileListItemDto>()
            .ForMember(
                d => d.Email,
                m => m.MapFrom(s => s.Email)
            ).ForMember(
                d => d.FirstName,
                m => m.MapFrom(s => s.FirstName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.LastName,
                m => m.MapFrom(s => s.LastName)
            ).ForMember(
                d => d.Roles,
                m => m.MapFrom(s => s.UserRoles.Where(ur => ur.IsActive).Select(ur => ur.Role.Name))
            );
    }
}