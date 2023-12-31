using FluentValidation;

namespace Users.Application.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileCommandValidator
    : AbstractValidator<DeleteUserProfileCommand>
{
    public DeleteUserProfileCommandValidator()
    {
        RuleFor(v => v.DeleteUserProfileRequest.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotNull().WithMessage("Id is required.");
    }
}