using Ads.Query.Application.Features.MyAds.GetMyAdList;
using FluentValidation;

namespace Ads.Query.Application.Features.GetClassifiedAds;

public class GetMyAdListQueryValidator
    : AbstractValidator<GetMyAdListQuery>
{
    public GetMyAdListQueryValidator()
    {
        RuleFor(v => v.GetMyAdListRequest.DataTableRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetMyAdListRequest.DataTableRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");
    }
}