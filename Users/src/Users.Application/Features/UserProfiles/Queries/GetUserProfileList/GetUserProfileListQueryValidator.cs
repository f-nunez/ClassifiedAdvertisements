using FluentValidation;

namespace Users.Application.Features.UserProfiles.Queries.GetUserProfileList;

public class GetUserProfileListQueryValidator
    : AbstractValidator<GetUserProfileListQuery>
{
    public GetUserProfileListQueryValidator()
    {
        RuleFor(v => v.GetUserProfileListRequest.DataTableRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetUserProfileListRequest.DataTableRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");
    }
}