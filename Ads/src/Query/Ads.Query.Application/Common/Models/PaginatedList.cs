namespace Ads.Query.Application.Common.Models;

public class PaginatedList<T>
{
    public long Count { get; private set; }
    public IEnumerable<T> Items { get; private set; }
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }

    public PaginatedList(int pageIndex, int pageSize, long count, IEnumerable<T> items)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Items = items;
    }
}