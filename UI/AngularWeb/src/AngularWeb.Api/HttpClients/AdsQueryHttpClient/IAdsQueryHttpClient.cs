using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdDetail;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdList;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdUpdate;

namespace AngularWeb.Api.HttpClients;

public interface IAdsQueryHttpClient
{
    Task<GetMyAdDetailResponse> GetMyAdDetailAsync(string id, CancellationToken cancellationToken);
    Task<GetMyAdListResponse> GetMyAdListAsync(int skip, int take, bool sortAsc, string sortProp, CancellationToken cancellationToken);
    Task<GetMyAdUpdateResponse> GetMyAdUpdateAsync(string id, CancellationToken cancellationToken);
}