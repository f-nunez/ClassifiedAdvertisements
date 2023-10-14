using Ads.Command.Application.Features.ClassifiedAds.CreateClassifiedAd;
using Ads.Command.Application.Features.ClassifiedAds.DeleteClassifiedAd;
using Ads.Command.Application.Features.ClassifiedAds.PublishClassifiedAd;
using Ads.Command.Application.Features.ClassifiedAds.UnpublishClassifiedAd;
using Ads.Command.Application.Features.ClassifiedAds.UpdateClassifiedAd;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Command.Api.Controllers;

public class ClassifiedAdsController : BaseApiController
{
    [HttpPost()]
    public async Task<IActionResult> Create(
        [FromBody] CreateClassifiedAdRequestParameter parameter,
        CancellationToken cancellationToken)
    {
        var command = new CreateClassifiedAdCommand(
            new CreateClassifiedAdRequest
            {
                Description = parameter.Description,
                Title = parameter.Title
            }
        );

        CreateClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{classifiedAdId}/version/{version}")]
    public async Task<IActionResult> Delete(
        string classifiedAdId,
        long version,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClassifiedAdCommand(
            new DeleteClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId,
                Version = version
            }
        );

        DeleteClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{classifiedAdId}/publish")]
    public async Task<IActionResult> Publish(
        string classifiedAdId,
        [FromBody] PublishClassifiedAdRequestParameter resource,
        CancellationToken cancellationToken)
    {
        var command = new PublishClassifiedAdCommand(
            new PublishClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId,
                Version = resource.Version
            }
        );

        PublishClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{classifiedAdId}/unpublish")]
    public async Task<IActionResult> Unpublish(
        string classifiedAdId,
        [FromBody] UnpublishClassifiedAdRequestParameter resource,
        CancellationToken cancellationToken)
    {
        var command = new UnpublishClassifiedAdCommand(
            new UnpublishClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId,
                Version = resource.Version
            }
        );

        UnpublishClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{classifiedAdId}")]
    public async Task<IActionResult> Update(
        string classifiedAdId,
        [FromBody] UpdateClassifiedAdRequestParameter resource,
        CancellationToken cancellationToken)
    {
        var command = new UpdateClassifiedAdCommand(
            new UpdateClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId,
                Description = resource.Description,
                Version = resource.Version,
                Title = resource.Title
            }
        );

        UpdateClassifiedAdResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}

public class CreateClassifiedAdRequestParameter
{
    public string? Description { get; set; }
    public string? Title { get; set; }
}

public class PublishClassifiedAdRequestParameter
{
    public long Version { get; set; }
}

public class UnpublishClassifiedAdRequestParameter
{
    public long Version { get; set; }
}

public class UpdateClassifiedAdRequestParameter
{
    public string? Description { get; set; }
    public string? Title { get; set; }
    public long Version { get; set; }
}