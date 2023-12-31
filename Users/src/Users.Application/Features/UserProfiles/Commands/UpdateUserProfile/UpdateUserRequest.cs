using Users.Application.Common.Requests;

namespace Users.Application.Features.UserProfiles.Commands.UpdateUserProfile;

public class UpdateUserProfileRequest : BaseRequest
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? Id { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImage { get; set; }
    public List<string> RoleIds { get; set; } = [];
    public string? UserName { get; set; }
}