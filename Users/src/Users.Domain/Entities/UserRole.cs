using Users.Domain.Common;

namespace Users.Domain.Entities;

public class UserRole : BaseEntity
{
    public string UserId { get; set; } = default!;
    public string RoleId { get; set; } = default!;
}