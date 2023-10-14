using FluentValidation;

namespace Ads.Command.Application.Features.DeleteMyAd;

public class DeleteMyAdCommandValidator
    : AbstractValidator<DeleteMyAdCommand>
{
    public DeleteMyAdCommandValidator()
    {
        RuleFor(v => v.DeleteMyAdRequest.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotNull().WithMessage("Id is required.");

        RuleFor(v => v.DeleteMyAdRequest.Version)
            .GreaterThan(-1).WithMessage("Version is required.");
    }
}