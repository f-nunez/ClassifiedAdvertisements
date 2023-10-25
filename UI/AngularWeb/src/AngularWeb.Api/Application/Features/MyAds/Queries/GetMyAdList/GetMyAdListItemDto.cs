namespace AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdList;

public class GetMyAdListItemDto
{
    public string? Description { get; set; }
    public string? Id { get; set; }
    public DateTimeOffset? PublishedOn { get; set; }
    public string? Title { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public long Version { get; set; }
}