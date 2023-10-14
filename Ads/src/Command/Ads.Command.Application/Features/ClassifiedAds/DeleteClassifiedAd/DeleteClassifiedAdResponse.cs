using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.DeleteClassifiedAd;

public class DeleteClassifiedAdResponse : BaseResponse
{
    public DeleteClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}