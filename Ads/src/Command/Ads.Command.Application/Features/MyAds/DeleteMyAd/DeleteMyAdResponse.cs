using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.DeleteMyAd;

public class DeleteMyAdResponse : BaseResponse
{
    public DeleteMyAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}