using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.GetClassifiedAd;

public class GetClassifiedAdResponse : BaseResponse
{
    public ClassifiedAdDto? ClassifiedAd { get; set; }

    public GetClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}