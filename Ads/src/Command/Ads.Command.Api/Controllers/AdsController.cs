using Ads.Command.Application.Features.CreateClassifiedAd;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Command.Api.Controllers;

public class AdsController : BaseApiController
{
    [HttpPost()]
    public async Task<IActionResult> Create(
        [FromBody] CreateClassifiedAdRequestResource resource,
        CancellationToken cancellationToken)
    {
        var command = new CreateClassifiedAdCommand(
            new CreateClassifiedAdRequest
            {
                Description = resource.Description,
                Title = resource.Title
            }
        );

        CreateClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}