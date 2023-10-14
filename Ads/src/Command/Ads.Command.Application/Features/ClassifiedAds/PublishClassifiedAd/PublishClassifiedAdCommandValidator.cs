using FluentValidation;

namespace Ads.Command.Application.Features.ClassifiedAds.PublishClassifiedAd;

public class PublishClassifiedAdCommandValidator
    : AbstractValidator<PublishClassifiedAdCommand>
{
    public PublishClassifiedAdCommandValidator()
    {
        RuleFor(v => v.PublishClassifiedAdRequest.ClassifiedAdId)
            .NotEmpty().WithMessage("ClassifiedAdId is required.")
            .NotNull().WithMessage("ClassifiedAdId is required.");

        RuleFor(v => v.PublishClassifiedAdRequest.Version)
            .GreaterThan(-1).WithMessage("Version is required.");
    }
}