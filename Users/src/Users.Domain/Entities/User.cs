using Users.Domain.Common;

namespace Users.Domain.Entities;

public class User : BaseEntity
{
    public string? FullName { get; set; }
    public string? NormalizedFullName { get; set; }
    public string? FirstName { get; set; }
    public string? NormalizedFirstName { get; set; }
    public string? LastName { get; set; }
    public string? NormalizedLastName { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public string? UserName { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
}