using AngularWeb.Api.Application.Common.Requests;

namespace AngularWeb.Api.Application.Features.MyAds.Commands.UpdateMyAd;

public class UpdateMyAdResponse : BaseResponse
{
    public UpdateMyAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}