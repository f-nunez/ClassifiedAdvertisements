using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.CreateClassifiedAd;

public class CreateClassifiedAdRequest : BaseRequest
{
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}