namespace Users.Application.Common.Requests;

public abstract class BaseMessage
{
    protected Guid _correlationId = Guid.NewGuid();
    public Guid CorrelationId => _correlationId;
}