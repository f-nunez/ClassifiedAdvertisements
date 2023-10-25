using AngularWeb.Api.Application.Common.Requests;

namespace AngularWeb.Api.Application.Features.MyAds.Commands.CreateMyAd;

public class CreateMyAdResponse : BaseResponse
{
    public CreateMyAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}