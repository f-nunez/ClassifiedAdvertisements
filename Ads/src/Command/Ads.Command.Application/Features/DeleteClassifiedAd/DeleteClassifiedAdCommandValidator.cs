using FluentValidation;

namespace Ads.Command.Application.Features.DeleteClassifiedAd;

public class DeleteClassifiedAdCommandValidator
    : AbstractValidator<DeleteClassifiedAdCommand>
{
    public DeleteClassifiedAdCommandValidator()
    {
        RuleFor(v => v.DeleteClassifiedAdRequest.ClassifiedAdId)
            .NotEmpty().WithMessage("ClassifiedAdId is required.")
            .NotNull().WithMessage("ClassifiedAdId is required.");

        RuleFor(v => v.DeleteClassifiedAdRequest.ExpectedVersion)
            .GreaterThan(-1).WithMessage("ExpectedVersion is required.");
    }
}