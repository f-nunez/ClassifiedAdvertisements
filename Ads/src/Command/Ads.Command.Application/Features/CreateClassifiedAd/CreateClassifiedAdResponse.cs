using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.CreateClassifiedAd;

public class CreateClassifiedAdResponse : BaseResponse
{
    public CreateClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}