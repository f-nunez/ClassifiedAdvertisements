using FluentValidation;

namespace Users.Application.Features.Users.Queries.GetFullNames;

public class GetFullNamesQueryValidator
    : AbstractValidator<GetFullNamesQuery>
{
    public GetFullNamesQueryValidator()
    {
        RuleFor(v => v.GetFullNamesRequest.Ids)
            .NotEmpty().WithMessage("Ids is required.")
            .NotNull().WithMessage("Ids is required.");
    }
}