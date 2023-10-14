using FluentValidation;

namespace Ads.Command.Application.Features.ClassifiedAds.UnpublishClassifiedAd;

public class UnpublishClassifiedAdCommandValidator
    : AbstractValidator<UnpublishClassifiedAdCommand>
{
    public UnpublishClassifiedAdCommandValidator()
    {
        RuleFor(v => v.UnpublishClassifiedAdRequest.ClassifiedAdId)
            .NotEmpty().WithMessage("ClassifiedAdId is required.")
            .NotNull().WithMessage("ClassifiedAdId is required.");

        RuleFor(v => v.UnpublishClassifiedAdRequest.Version)
            .GreaterThan(-1).WithMessage("Version is required.");
    }
}