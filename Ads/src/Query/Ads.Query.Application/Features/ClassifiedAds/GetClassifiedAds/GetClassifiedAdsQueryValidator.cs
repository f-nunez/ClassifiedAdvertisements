using FluentValidation;

namespace Ads.Query.Application.Features.ClassifiedAds.GetClassifiedAds;

public class GetClassifiedAdsQueryValidator : AbstractValidator<GetClassifiedAdsQuery>
{
    public GetClassifiedAdsQueryValidator()
    {
        RuleFor(v => v.GetClassifiedAdsRequest.PageIndex)
            .GreaterThanOrEqualTo(0).WithMessage("PageIndex must be greater than or equal to 0.");

        RuleFor(v => v.GetClassifiedAdsRequest.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("PageSize must be less than or equal to 100.");
    }
}