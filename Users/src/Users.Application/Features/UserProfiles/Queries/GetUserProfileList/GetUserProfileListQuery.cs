using MediatR;

namespace Users.Application.Features.UserProfiles.Queries.GetUserProfileList;

public record GetUserProfileListQuery(
    GetUserProfileListRequest GetUserProfileListRequest)
    : IRequest<GetUserProfileListResponse>;