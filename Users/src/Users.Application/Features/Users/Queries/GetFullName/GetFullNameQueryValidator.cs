using FluentValidation;

namespace Users.Application.Features.Users.Queries.GetFullName;

public class GetFullNameQueryValidator
    : AbstractValidator<GetFullNameQuery>
{
    public GetFullNameQueryValidator()
    {
        RuleFor(v => v.GetFullNameRequest.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotNull().WithMessage("Id is required.");
    }
}