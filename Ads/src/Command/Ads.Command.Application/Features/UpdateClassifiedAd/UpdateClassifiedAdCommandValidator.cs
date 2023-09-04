using FluentValidation;

namespace Ads.Command.Application.Features.UpdateClassifiedAd;

public class UpdateClassifiedAdCommandValidator
    : AbstractValidator<UpdateClassifiedAdCommand>
{
    public UpdateClassifiedAdCommandValidator()
    {
        RuleFor(v => v.UpdateClassifiedAdRequest.ClassifiedAdId)
            .NotEmpty().WithMessage("ClassifiedAdId is required.")
            .NotNull().WithMessage("ClassifiedAdId is required.");

        RuleFor(v => v.UpdateClassifiedAdRequest.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull().WithMessage("Description is required.")
            .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters.");

        RuleFor(v => v.UpdateClassifiedAdRequest.ExpectedVersion)
            .GreaterThan(-1).WithMessage("ExpectedVersion is required.");

        RuleFor(v => v.UpdateClassifiedAdRequest.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
    }
}