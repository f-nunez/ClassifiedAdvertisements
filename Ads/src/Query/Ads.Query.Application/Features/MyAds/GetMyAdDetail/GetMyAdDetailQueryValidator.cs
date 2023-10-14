using FluentValidation;

namespace Ads.Query.Application.Features.MyAds.GetMyAdDetail;

public class GetMyAdDetailQueryValidator
    : AbstractValidator<GetMyAdDetailQuery>
{
    public GetMyAdDetailQueryValidator()
    {
        RuleFor(v => v.GetMyAdDetailRequest.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotNull().WithMessage("Id is required.");
    }
}