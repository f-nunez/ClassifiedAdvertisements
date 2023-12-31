namespace Users.Domain.Common;

public abstract class BaseEntity
{
    public string? Id { get; set; }
    public bool IsActive { get; set; } = true;
}