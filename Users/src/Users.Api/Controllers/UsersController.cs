using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features.Users.Queries.GetFullName;
using Users.Application.Features.Users.Queries.GetFullNames;

namespace Users.Api.Controllers;

[Authorize("GeneralPolicy")]
public class UsersController : BaseApiController
{
    [HttpGet("GetUserFullName")]
    public async Task<IActionResult> GetUserFullName(
        [FromQuery] string id,
        CancellationToken cancellationToken)
    {
        var query = new GetFullNameQuery(
            new GetFullNameRequest { Id = id }
        );

        GetFullNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetUserFullNames")]
    public async Task<IActionResult> GetUserFullNames(
        string[] ids,
        CancellationToken cancellationToken)
    {
        var query = new GetFullNamesQuery(
            new GetFullNamesRequest { Ids = ids }
        );

        GetFullNamesResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}