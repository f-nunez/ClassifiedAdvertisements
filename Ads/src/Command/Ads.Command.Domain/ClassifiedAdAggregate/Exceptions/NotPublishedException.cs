namespace Ads.Command.Domain.ClassifiedAdAggregate.Exceptions;

public class NotPublishedException : Exception
{
    public NotPublishedException() : base()
    {
    }

    public NotPublishedException(string message) : base(message)
    {
    }

    public NotPublishedException(string name, object? key)
        : base($"{name} ({key}) is not published")
    {
    }
}