using Ads.Query.Application.Features.GetClassifiedAd;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Query.Api.Controllers;

public class AdsController : BaseApiController
{
    [HttpGet("{classifiedAdId}")]
    public async Task<IActionResult> GetClassifiedAd(
        string classifiedAdId,
        CancellationToken cancellationToken)
    {
        var query = new GetClassifiedAdQuery(
            new GetClassifiedAdRequest
            {
                ClassifiedAdId = classifiedAdId
            }
        );

        GetClassifiedAdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}