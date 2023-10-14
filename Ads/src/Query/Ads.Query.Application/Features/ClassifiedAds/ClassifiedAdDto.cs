namespace Ads.Query.Application.Features.ClassifiedAds;

public class ClassifiedAdDto
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string? Description { get; set; }
    public string? Id { get; set; }
    public bool IsActive { get; set; }
    public string? PublishedBy { get; set; }
    public DateTimeOffset? PublishedOn { get; set; }
    public string? Title { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public long Version { get; set; }
}