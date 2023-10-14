using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.PublishClassifiedAd;

public class PublishClassifiedAdResponse : BaseResponse
{
    public PublishClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}