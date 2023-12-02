using Users.Application.Common.Requests;

namespace Users.Application.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileRequest : BaseRequest
{
    public string? Id { get; set; }
}