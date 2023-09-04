using Ads.Command.Application.Features.CreateClassifiedAd;
using Ads.Command.Application.Features.DeleteClassifiedAd;
using Ads.Command.Application.Features.PublishClassifiedAd;
using Ads.Command.Application.Features.UnpublishClassifiedAd;
using Ads.Command.Application.Features.UpdateClassifiedAd;
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

    [HttpDelete("{ClassifiedAdId}/version/{ExpectedVersion}")]
    public async Task<IActionResult> Delete(
        [FromRoute] DeleteClassifiedAdRequestResource resource,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClassifiedAdCommand(
            new DeleteClassifiedAdRequest
            {
                ClassifiedAdId = resource.ClassifiedAdId,
                ExpectedVersion = resource.ExpectedVersion
            }
        );

        DeleteClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{ClassifiedAdId}/publish")]
    public async Task<IActionResult> Publish(
        string classifiedAdId,
        [FromBody] PublishClassifiedAdRequestResource resource,
        CancellationToken cancellationToken)
    {
        var command = new PublishClassifiedAdCommand(
            new PublishClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId,
                ExpectedVersion = resource.ExpectedVersion
            }
        );

        PublishClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{ClassifiedAdId}/unpublish")]
    public async Task<IActionResult> Unpublish(
        string classifiedAdId,
        [FromBody] UnpublishClassifiedAdRequestResource resource,
        CancellationToken cancellationToken)
    {
        var command = new UnpublishClassifiedAdCommand(
            new UnpublishClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId,
                ExpectedVersion = resource.ExpectedVersion
            }
        );

        UnpublishClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{ClassifiedAdId}")]
    public async Task<IActionResult> Update(
        string classifiedAdId,
        [FromBody] UpdateClassifiedAdRequestResource resource,
        CancellationToken cancellationToken)
    {
        var command = new UpdateClassifiedAdCommand(
            new UpdateClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId,
                Description = resource.Description,
                ExpectedVersion = resource.ExpectedVersion,
                Title = resource.Title
            }
        );

        UpdateClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}