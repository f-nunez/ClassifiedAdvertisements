using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Common.Requests;
using Users.Application.Features.UserProfiles.Commands.CreateUserProfile;
using Users.Application.Features.UserProfiles.Commands.UpdateUserProfile;

namespace Users.Api.Controllers;

[Authorize("GeneralPolicy")]
public class UserProfilesController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateUserProfile(
        [FromBody] CreateUserProfileRequestParameter parameter,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserProfileCommand(
            new CreateUserProfileRequest
            {
                Email = parameter.Email,
                FirstName = parameter.FirstName,
                LastName = parameter.LastName,
                PhoneNumber = parameter.PhoneNumber,
                ProfileImage = parameter.ProfileImage,
                RoleIds = parameter.RoleIds,
                UserName = parameter.UserName
            }
        );

        CreateUserProfileResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserProfile(
        string id,
        [FromBody] UpdateUserProfileRequestParameter parameter,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserProfileCommand(
            new UpdateUserProfileRequest
            {
                Email = parameter.Email,
                FirstName = parameter.FirstName,
                Id = id,
                LastName = parameter.LastName,
                PhoneNumber = parameter.PhoneNumber,
                ProfileImage = parameter.ProfileImage,
                RoleIds = parameter.RoleIds,
                UserName = parameter.UserName
            }
        );

        UpdateUserProfileResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}

public class CreateUserProfileRequestParameter
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImage { get; set; }
    public List<string> RoleIds { get; set; } = [];
    public string? UserName { get; set; }
}

public class UpdateUserProfileRequestParameter
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImage { get; set; }
    public List<string> RoleIds { get; set; } = [];
    public string? UserName { get; set; }
}