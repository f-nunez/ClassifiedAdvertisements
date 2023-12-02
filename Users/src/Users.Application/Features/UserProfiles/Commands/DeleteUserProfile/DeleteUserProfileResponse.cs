using Users.Application.Common.Requests;

namespace Users.Application.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileResponse : BaseResponse
{
    public DeleteUserProfileResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}