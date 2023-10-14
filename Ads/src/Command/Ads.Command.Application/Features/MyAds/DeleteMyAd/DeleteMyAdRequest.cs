using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.DeleteMyAd;

public class DeleteMyAdRequest : BaseRequest
{
    public string? Id { get; set; }
    public long Version { get; set; }
}