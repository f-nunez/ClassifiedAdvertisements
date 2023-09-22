using Ads.Query.Domain.Common;

namespace Ads.Query.Domain.Entities;

public class ClassifiedAd : BaseEntity
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string? Description { get; set; }
    public string? PublishedBy { get; set; }
    public DateTimeOffset? PublishedOn { get; set; }
    public string? Title { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
}