using FluentValidation;

namespace Ads.Query.Application.Features.MyAds.GetMyAdUpdate;

public class GetMyAdUpdateQueryValidator
    : AbstractValidator<GetMyAdUpdateQuery>
{
    public GetMyAdUpdateQueryValidator()
    {
        RuleFor(v => v.GetMyAdUpdateRequest.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotNull().WithMessage("Id is required.");
    }
}