using AngularWeb.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngularWeb.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MyAdsController : ControllerBase
{
    private readonly IMyAdsService _myAdsService;

    public MyAdsController(IMyAdsService myAdsService)
    {
        _myAdsService = myAdsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMyAd(
        [FromBody] CreateMyAdRequestParameter parameter,
        CancellationToken cancellationToken)
    {
        var response = await _myAdsService.CreateMyAdAsync(
            parameter.Description,
            parameter.Title,
            cancellationToken
        );

        return Ok(response);
    }

    [HttpDelete("{id}/version/{version}")]
    public async Task<IActionResult> DeleteMyAd(
        string id,
        long version,
        CancellationToken cancellationToken)
    {
        var response = await _myAdsService.DeleteMyAdAsync(
            id,
            version,
            cancellationToken
        );

        return Ok(response);
    }

    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetMyAdDetail(
        string id,
        CancellationToken cancellationToken)
    {
        var response = await _myAdsService
            .GetMyAdDetailAsync(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyAdList(
        int skip,
        int take,
        bool sortAsc,
        string sortProp,
        CancellationToken cancellationToken)
    {
        var response = await _myAdsService.GetMyAdListAsync(
            skip,
            take,
            sortAsc,
            sortProp,
            cancellationToken
        );

        return Ok(response);
    }

    [HttpGet("{id}/update")]
    public async Task<IActionResult> GetMyAdUpdate(
        string id,
        CancellationToken cancellationToken)
    {
        var response = await _myAdsService
            .GetMyAdUpdateAsync(id, cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMyAd(
        string id,
        [FromBody] UpdateMyAdRequestParameter parameter,
        CancellationToken cancellationToken)
    {
        var response = await _myAdsService.UpdateMyAdAsync(
            id,
            parameter.Description,
            parameter.Title,
            parameter.Version,
            cancellationToken
        );

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