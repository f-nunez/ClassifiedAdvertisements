namespace Ads.Query.Application.Common.Requests;

public class DataTableRequest
{
    public int Skip { get; set; }
    public List<DataTableRequestSort>? Sorts { get; set; }
    public int Take { get; set; }
}