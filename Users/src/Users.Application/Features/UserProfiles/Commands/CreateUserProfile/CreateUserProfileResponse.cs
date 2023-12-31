using Users.Application.Common.Requests;

namespace Users.Application.Features.UserProfiles.Commands.CreateUserProfile;

public class CreateUserProfileResponse : BaseResponse
{
    public CreateUserProfileResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}