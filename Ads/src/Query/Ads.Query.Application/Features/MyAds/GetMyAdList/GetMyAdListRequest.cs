using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.MyAds.GetMyAdList;

public class GetMyAdListRequest : BaseRequest
{
    public DataTableRequest DataTableRequest { get; set; } = new();
}