using MediatR;

namespace Users.Application.Features.UserProfiles.Commands.UpdateUserProfile;

public record UpdateUserProfileCommand(
    UpdateUserProfileRequest UpdateUserProfileRequest)
    : IRequest<UpdateUserProfileResponse>;