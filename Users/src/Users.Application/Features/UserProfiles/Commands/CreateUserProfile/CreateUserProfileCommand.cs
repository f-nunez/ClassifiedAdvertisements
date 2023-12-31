using MediatR;

namespace Users.Application.Features.UserProfiles.Commands.CreateUserProfile;

public record CreateUserProfileCommand(
    CreateUserProfileRequest CreateUserProfileRequest)
    : IRequest<CreateUserProfileResponse>;