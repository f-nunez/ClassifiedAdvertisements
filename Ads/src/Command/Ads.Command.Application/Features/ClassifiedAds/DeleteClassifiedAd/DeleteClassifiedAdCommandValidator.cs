using FluentValidation;

namespace Ads.Command.Application.Features.ClassifiedAds.DeleteClassifiedAd;

public class DeleteClassifiedAdCommandValidator
    : AbstractValidator<DeleteClassifiedAdCommand>
{
    public DeleteClassifiedAdCommandValidator()
    {
        RuleFor(v => v.DeleteClassifiedAdRequest.ClassifiedAdId)
            .NotEmpty().WithMessage("ClassifiedAdId is required.")
            .NotNull().WithMessage("ClassifiedAdId is required.");

        RuleFor(v => v.DeleteClassifiedAdRequest.Version)
            .GreaterThan(-1).WithMessage("Version is required.");
    }
}