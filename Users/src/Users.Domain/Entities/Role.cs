using Users.Domain.Common;

namespace Users.Domain.Entities;

public class Role : BaseEntity
{
    public string? Name { get; set; }
    public string? NormalizedName { get; set; }
}