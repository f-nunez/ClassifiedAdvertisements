using MediatR;

namespace Ads.Command.Application.Features.UpdateMyAd;

public record UpdateMyAdCommand(
    UpdateMyAdRequest UpdateMyAdRequest)
    : IRequest<UpdateMyAdResponse>;