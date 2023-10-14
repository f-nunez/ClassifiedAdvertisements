using Ads.Query.Application.Features.ClassifiedAds.GetClassifiedAd;
using Ads.Query.Application.Features.ClassifiedAds.GetClassifiedAds;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Query.Api.Controllers;

public class ClassifiedAdsController : BaseApiController
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

    [HttpGet]
    public async Task<IActionResult> GetClassifiedAds(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetClassifiedAdsQuery(
            new GetClassifiedAdsRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            }
        );

        GetClassifiedAdsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}