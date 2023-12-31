using FluentValidation;

namespace Users.Application.Features.UserProfiles.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandValidator
    : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.UpdateUserProfileRequest.Email)
            .NotEmpty().WithMessage("Email is required.")
            .NotNull().WithMessage("Email is required.")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");

        RuleFor(v => v.UpdateUserProfileRequest.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .NotNull().WithMessage("FirstName is required.")
            .MaximumLength(255).WithMessage("FirstName must not exceed 255 characters.");

        RuleFor(v => v.UpdateUserProfileRequest.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotNull().WithMessage("Id is required.");

        RuleFor(v => v.UpdateUserProfileRequest.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .NotNull().WithMessage("LastName is required.")
            .MaximumLength(255).WithMessage("LastName must not exceed 255 characters.");

        RuleFor(v => v.UpdateUserProfileRequest.PhoneNumber)
            .MaximumLength(255).WithMessage("PhoneNumber must not exceed 255 characters.");

        RuleFor(v => v.UpdateUserProfileRequest.ProfileImage)
            .MaximumLength(255).WithMessage("ProfileImageUrl must not exceed 255 characters.");

        RuleFor(v => v.UpdateUserProfileRequest.RoleIds)
            .NotEmpty().WithMessage("RoleIds is required.")
            .NotNull().WithMessage("RoleIds is required.");

        RuleFor(v => v.UpdateUserProfileRequest.UserName)
            .NotEmpty().WithMessage("UserName is required.")
            .NotNull().WithMessage("UserName is required.")
            .MaximumLength(255).WithMessage("UserName must not exceed 255 characters.");
    }
}