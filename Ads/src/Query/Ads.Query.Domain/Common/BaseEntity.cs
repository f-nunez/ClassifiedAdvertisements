namespace Ads.Query.Domain.Common;

public abstract class BaseEntity
{
    public string? Id { get; set; }
    public bool IsActive { get; set; }
    public long Version { get; set; }
}