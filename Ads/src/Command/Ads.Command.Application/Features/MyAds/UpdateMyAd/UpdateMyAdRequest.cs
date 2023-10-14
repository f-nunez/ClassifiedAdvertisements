using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.UpdateMyAd;

public class UpdateMyAdRequest : BaseRequest
{
    public string? Description { get; set; }
    public string? Id { get; set; }
    public string? Title { get; set; }
    public long Version { get; set; }
}