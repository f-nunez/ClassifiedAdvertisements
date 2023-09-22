namespace Ads.Common.DomainEvents;

public class ClassifiedAdDeletedV1 : BaseDomainEvent
{
    public string? DeletedBy { get; set; }
    public DateTimeOffset DeletedOn { get; set; }
}