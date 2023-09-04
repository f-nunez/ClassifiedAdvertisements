using MediatR;

namespace Ads.Command.Application.Features.UpdateClassifiedAd;

public record UpdateClassifiedAdCommand(UpdateClassifiedAdRequest UpdateClassifiedAdRequest)
    : IRequest<UpdateClassifiedAdResponse>;