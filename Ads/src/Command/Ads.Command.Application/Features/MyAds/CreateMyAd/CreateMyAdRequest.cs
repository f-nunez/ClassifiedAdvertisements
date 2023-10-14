using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.CreateMyAd;

public class CreateMyAdRequest : BaseRequest
{
    public string? Description { get; set; }
    public string? Title { get; set; }
}