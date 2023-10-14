using Ads.Command.Application.Features.CreateMyAd;
using Ads.Command.Application.Features.DeleteMyAd;
using Ads.Command.Application.Features.UpdateMyAd;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Command.Api.Controllers;

public class MyAdsController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateMyAd(
        [FromBody] CreateMyAdRequestParameter parameter,
        CancellationToken cancellationToken)
    {
        var command = new CreateMyAdCommand(
            new CreateMyAdRequest
            {
                Description = parameter.Description,
                Title = parameter.Title
            }
        );

        CreateMyAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id}/version/{version}")]
    public async Task<IActionResult> DeleteMyAd(
        string id,
        long version,
        CancellationToken cancellationToken)
    {
        var command = new DeleteMyAdCommand(
            new DeleteMyAdRequest
            {
                Id = id,
                Version = version
            }
        );

        DeleteMyAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMyAd(
        string id,
        [FromBody] UpdateMyAdRequestParameter parameter,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMyAdCommand(
            new UpdateMyAdRequest
            {
                Description = parameter.Description,
                Id = id,
                Title = parameter.Title,
                Version = parameter.Version
            }
        );

        UpdateMyAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}

public class CreateMyAdRequestParameter
{
    public string? Description { get; set; }
    public string? Title { get; set; }
}

public class UpdateMyAdRequestParameter
{
    public string? Description { get; set; }
    public string? Title { get; set; }
    public long Version { get; set; }
}