using AngularWeb.Api.Application.Common.Requests;

namespace AngularWeb.Api.Application.Features.MyAds.Commands.DeleteMyAd;

public class DeleteMyAdResponse : BaseResponse
{
    public DeleteMyAdResponse(Guid correlationId) : base(correlationId)
    {
    }
}