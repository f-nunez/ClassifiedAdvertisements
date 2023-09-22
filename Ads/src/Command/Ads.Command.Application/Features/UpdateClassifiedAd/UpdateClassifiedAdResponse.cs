using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.UpdateClassifiedAd;

public class UpdateClassifiedAdResponse : BaseResponse
{
    public UpdateClassifiedAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}