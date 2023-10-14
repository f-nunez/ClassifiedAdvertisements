using FluentValidation;

namespace Ads.Command.Application.Features.CreateMyAd;

public class CreateMyAdCommandValidator
    : AbstractValidator<CreateMyAdCommand>
{
    public CreateMyAdCommandValidator()
    {
        RuleFor(v => v.CreateMyAdRequest.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull().WithMessage("Description is required.")
            .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters.");

        RuleFor(v => v.CreateMyAdRequest.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
    }
}