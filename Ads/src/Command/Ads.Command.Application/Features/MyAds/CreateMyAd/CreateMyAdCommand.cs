using MediatR;

namespace Ads.Command.Application.Features.CreateMyAd;

public record CreateMyAdCommand(
    CreateMyAdRequest CreateMyAdRequest)
    : IRequest<CreateMyAdResponse>;