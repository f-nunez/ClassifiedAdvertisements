using Users.Domain.Common;

namespace Users.Domain.Entities;

public class Role : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? NormalizedName { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
}