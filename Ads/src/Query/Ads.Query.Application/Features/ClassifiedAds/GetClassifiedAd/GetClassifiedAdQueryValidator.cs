using FluentValidation;

namespace Ads.Query.Application.Features.ClassifiedAds.GetClassifiedAd;

public class GetClassifiedAdQueryValidator
    : AbstractValidator<GetClassifiedAdQuery>
{
    public GetClassifiedAdQueryValidator()
    {
        RuleFor(v => v.GetClassifiedAdRequest.ClassifiedAdId)
            .NotEmpty().WithMessage("ClassifiedAdId is required.")
            .NotNull().WithMessage("ClassifiedAdId is required.");
    }
}