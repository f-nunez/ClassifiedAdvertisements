using FluentValidation;

namespace Ads.Command.Application.Features.UpdateMyAd;

public class UpdateMyAdCommandValidator
    : AbstractValidator<UpdateMyAdCommand>
{
    public UpdateMyAdCommandValidator()
    {
        RuleFor(v => v.UpdateMyAdRequest.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull().WithMessage("Description is required.")
            .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters.");

        RuleFor(v => v.UpdateMyAdRequest.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotNull().WithMessage("Id is required.");

        RuleFor(v => v.UpdateMyAdRequest.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(v => v.UpdateMyAdRequest.Version)
            .GreaterThan(-1).WithMessage("Version is required.");
    }
}