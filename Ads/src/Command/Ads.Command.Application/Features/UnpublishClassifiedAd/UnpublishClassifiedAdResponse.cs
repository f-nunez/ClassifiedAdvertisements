using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.UnpublishClassifiedAd;

public class UnpublishClassifiedAdResponse : BaseResponse
{
    public UnpublishClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}