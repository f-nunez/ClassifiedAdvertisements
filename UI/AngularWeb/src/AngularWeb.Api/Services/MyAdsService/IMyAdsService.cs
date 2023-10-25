using AngularWeb.Api.Application.Features.MyAds.Commands.CreateMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.DeleteMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.UpdateMyAd;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdDetail;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdList;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdUpdate;

namespace AngularWeb.Api.Services;

public interface IMyAdsService
{
    Task<CreateMyAdResponse> CreateMyAdAsync(string? description, string? title, CancellationToken cancellationToken);
    Task<DeleteMyAdResponse> DeleteMyAdAsync(string? id, long version, CancellationToken cancellationToken);
    Task<GetMyAdDetailResponse> GetMyAdDetailAsync(string id, CancellationToken cancellationToken);
    Task<GetMyAdListResponse> GetMyAdListAsync(int skip, int take, bool sortAsc, string sortProp, CancellationToken cancellationToken);
    Task<GetMyAdUpdateResponse> GetMyAdUpdateAsync(string id, CancellationToken cancellationToken);
    Task<UpdateMyAdResponse> UpdateMyAdAsync(string? id, string? description, string? title, long version, CancellationToken cancellationToken);
}