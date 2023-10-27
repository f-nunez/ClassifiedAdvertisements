namespace AngularWeb.Api.Application.Common.Requests;

public class DataTableRequestSort
{
    public bool IsAscending { get; set; }
    public string PropertyName { get; set; }

    public DataTableRequestSort(string propertyName, bool isAscending = true)
    {
        if (string.IsNullOrEmpty(propertyName))
            throw new ArgumentNullException(nameof(propertyName));

        PropertyName = propertyName;
        IsAscending = isAscending;
    }
}