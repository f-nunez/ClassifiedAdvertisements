namespace Ads.Query.Application.Features.ClassifiedAds;

public class ClassifiedAdPaginatedListItemDto
{
    public string? Description { get; set; }
    public string? Id { get; set; }
    public string? PublishedBy { get; set; }
    public DateTimeOffset? PublishedOn { get; set; }
    public string? Title { get; set; }
}