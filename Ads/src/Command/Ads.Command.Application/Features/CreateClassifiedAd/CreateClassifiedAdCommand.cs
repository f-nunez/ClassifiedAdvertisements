using MediatR;

namespace Ads.Command.Application.Features.CreateClassifiedAd;

public record CreateClassifiedAdCommand(CreateClassifiedAdRequest CreateClassifiedAdRequest)
    : IRequest<CreateClassifiedAdResponse>;