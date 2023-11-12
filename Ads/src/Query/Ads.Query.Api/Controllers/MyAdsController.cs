using Ads.Query.Application.Common.Requests;
using Ads.Query.Application.Features.MyAds.GetMyAdDetail;
using Ads.Query.Application.Features.MyAds.GetMyAdList;
using Ads.Query.Application.Features.MyAds.GetMyAdUpdate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Query.Api.Controllers;

[Authorize("GeneralPolicy")]
public class MyAdsController : BaseApiController
{
    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetMyAdDetail(
        string id,
        CancellationToken cancellationToken)
    {
        var query = new GetMyAdDetailQuery(
            new GetMyAdDetailRequest
            {
                Id = id
            }
        );

        GetMyAdDetailResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyAdList(
        int skip,
        int take,
        bool sortasc,
        string sortprop,
        CancellationToken cancellationToken)
    {
        var query = new GetMyAdListQuery(
            new GetMyAdListRequest
            {
                DataTableRequest = new DataTableRequest
                {
                    Skip = skip,
                    Take = take,
                    Sorts = new List<DataTableRequestSort>()
                    {
                        new(sortprop, sortasc)
                    }
                }
            }
        );

        GetMyAdListResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id}/update")]
    public async Task<IActionResult> GetMyAdUpdate(
        string id,
        CancellationToken cancellationToken)
    {
        var query = new GetMyAdUpdateQuery(
            new GetMyAdUpdateRequest
            {
                Id = id
            }
        );

        GetMyAdUpdateResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}