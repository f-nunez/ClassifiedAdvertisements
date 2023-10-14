using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.CreateClassifiedAd;

public class CreateClassifiedAdResponse : BaseResponse
{
    public CreateClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}