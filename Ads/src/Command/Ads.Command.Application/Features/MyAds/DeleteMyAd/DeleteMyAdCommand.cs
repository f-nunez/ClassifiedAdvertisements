using MediatR;

namespace Ads.Command.Application.Features.DeleteMyAd;

public record DeleteMyAdCommand(
    DeleteMyAdRequest DeleteMyAdRequest)
    : IRequest<DeleteMyAdResponse>;