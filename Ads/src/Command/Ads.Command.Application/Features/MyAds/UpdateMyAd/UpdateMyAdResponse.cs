using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.UpdateMyAd;

public class UpdateMyAdResponse : BaseResponse
{
    public UpdateMyAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}