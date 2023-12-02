using MediatR;

namespace Users.Application.Features.UserProfiles.Commands.DeleteUserProfile;

public record DeleteUserProfileCommand(
    DeleteUserProfileRequest DeleteUserProfileRequest)
    : IRequest<DeleteUserProfileResponse>;