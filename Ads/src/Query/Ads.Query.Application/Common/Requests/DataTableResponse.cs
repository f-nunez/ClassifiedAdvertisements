namespace Ads.Query.Application.Common.Requests;

public class DataTableResponse<T>
{
    public long Count { get; private set; }
    public IEnumerable<T> Items { get; private set; }

    public DataTableResponse(IEnumerable<T> items, long count)
    {
        Items = items;
        Count = count;
    }
}