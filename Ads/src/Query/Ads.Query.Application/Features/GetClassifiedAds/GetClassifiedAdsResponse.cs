using Ads.Query.Application.Common.Models;
using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.GetClassifiedAds;

public class GetClassifiedAdsResponse : BaseResponse
{
    public PaginatedList<ClassifiedAdPaginatedListItemDto>? PaginatedList { get; set; }

    public GetClassifiedAdsResponse(Guid correlationId) : base(correlationId)
    {
    }
}