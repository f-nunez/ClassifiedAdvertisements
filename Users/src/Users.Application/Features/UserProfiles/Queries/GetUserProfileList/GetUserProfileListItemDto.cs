namespace Users.Application.Features.UserProfiles.Queries.GetUserProfileList;

public class GetUserProfileListItemDto
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? Id { get; set; }
    public string? LastName { get; set; }
    public IEnumerable<string> Roles { get; set; } = [];
}