using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.CreateMyAd;

public class CreateMyAdResponse : BaseResponse
{
    public CreateMyAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}