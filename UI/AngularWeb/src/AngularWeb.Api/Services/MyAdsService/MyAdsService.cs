using AngularWeb.Api.Application.Features.MyAds.Commands.CreateMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.DeleteMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.UpdateMyAd;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdDetail;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdList;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdUpdate;
using AngularWeb.Api.HttpClients;

namespace AngularWeb.Api.Services;

public class MyAdsService : IMyAdsService
{
    private readonly IAdsCommandHttpClient _adsCommandHttpClient;
    private readonly IAdsQueryHttpClient _adsQueryHttpClient;

    public MyAdsService(
        IAdsCommandHttpClient adsCommandHttpClient,
        IAdsQueryHttpClient adsQueryHttpClient)
    {
        _adsCommandHttpClient = adsCommandHttpClient;
        _adsQueryHttpClient = adsQueryHttpClient;
    }

    public async Task<CreateMyAdResponse> CreateMyAdAsync(
        string? description,
        string? title,
        CancellationToken cancellationToken)
    {
        return await _adsCommandHttpClient.CreateMyAdAsync(
            description,
            title,
            cancellationToken
        );
    }

    public async Task<DeleteMyAdResponse> DeleteMyAdAsync(
        string? id,
        long version,
        CancellationToken cancellationToken)
    {
        return await _adsCommandHttpClient.DeleteMyAdAsync(
            id,
            version,
            cancellationToken
        );
    }

    public async Task<GetMyAdDetailResponse> GetMyAdDetailAsync(
        string id,
        CancellationToken cancellationToken)
    {
        return await _adsQueryHttpClient.GetMyAdDetailAsync(id, cancellationToken);
    }

    public async Task<GetMyAdListResponse> GetMyAdListAsync(
        int skip,
        int take,
        bool sortAsc,
        string sortProp,
        CancellationToken cancellationToken)
    {
        return await _adsQueryHttpClient.GetMyAdListAsync(
            skip,
            take,
            sortAsc,
            sortProp,
            cancellationToken
        );
    }

    public async Task<GetMyAdUpdateResponse> GetMyAdUpdateAsync(
        string id,
        CancellationToken cancellationToken)
    {
        return await _adsQueryHttpClient.GetMyAdUpdateAsync(id, cancellationToken);
    }

    public async Task<UpdateMyAdResponse> UpdateMyAdAsync(
        string? id,
        string? description,
        string? title,
        long version,
        CancellationToken cancellationToken)
    {
        return await _adsCommandHttpClient.UpdateMyAdAsync(
            id,
            description,
            title,
            version,
            cancellationToken
        );
    }
}