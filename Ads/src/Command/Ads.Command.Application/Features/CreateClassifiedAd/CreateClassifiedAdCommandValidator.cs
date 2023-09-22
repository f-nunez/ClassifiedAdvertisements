using FluentValidation;

namespace Ads.Command.Application.Features.CreateClassifiedAd;

public class CreateClassifiedAdCommandValidator
    : AbstractValidator<CreateClassifiedAdCommand>
{
    public CreateClassifiedAdCommandValidator()
    {
        RuleFor(v => v.CreateClassifiedAdRequest.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull().WithMessage("Description is required.")
            .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters.");

        RuleFor(v => v.CreateClassifiedAdRequest.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
    }
}