using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.PublishClassifiedAd;

public class PublishClassifiedAdResponse : BaseResponse
{
    public PublishClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}