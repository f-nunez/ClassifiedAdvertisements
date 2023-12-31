using Users.Application.Common.Requests;

namespace Users.Application.Features.UserProfiles.Commands.UpdateUserProfile;

public class UpdateUserProfileResponse : BaseResponse
{
    public UpdateUserProfileResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}