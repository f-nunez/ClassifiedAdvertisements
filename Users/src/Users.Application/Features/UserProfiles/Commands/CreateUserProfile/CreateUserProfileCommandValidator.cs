using FluentValidation;

namespace Users.Application.Features.UserProfiles.Commands.CreateUserProfile;

public class CreateUserProfileCommandValidator
    : AbstractValidator<CreateUserProfileCommand>
{
    public CreateUserProfileCommandValidator()
    {
        RuleFor(v => v.CreateUserProfileRequest.Email)
            .NotEmpty().WithMessage("Email is required.")
            .NotNull().WithMessage("Email is required.")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");

        RuleFor(v => v.CreateUserProfileRequest.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .NotNull().WithMessage("FirstName is required.")
            .MaximumLength(255).WithMessage("FirstName must not exceed 255 characters.");

        RuleFor(v => v.CreateUserProfileRequest.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .NotNull().WithMessage("LastName is required.")
            .MaximumLength(255).WithMessage("LastName must not exceed 255 characters.");

        RuleFor(v => v.CreateUserProfileRequest.PhoneNumber)
            .MaximumLength(255).WithMessage("PhoneNumber must not exceed 255 characters.");

        RuleFor(v => v.CreateUserProfileRequest.ProfileImage)
            .MaximumLength(255).WithMessage("ProfileImageUrl must not exceed 255 characters.");

        RuleFor(v => v.CreateUserProfileRequest.RoleIds)
            .NotEmpty().WithMessage("RoleIds is required.")
            .NotNull().WithMessage("RoleIds is required.");

        RuleFor(v => v.CreateUserProfileRequest.UserName)
            .NotEmpty().WithMessage("UserName is required.")
            .NotNull().WithMessage("UserName is required.")
            .MaximumLength(255).WithMessage("UserName must not exceed 255 characters.");
    }
}