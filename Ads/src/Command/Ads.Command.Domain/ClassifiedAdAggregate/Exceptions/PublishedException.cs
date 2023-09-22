namespace Ads.Command.Domain.ClassifiedAdAggregate.Exceptions;

public class PublishedException : Exception
{
    public PublishedException() : base()
    {
    }

    public PublishedException(string message) : base(message)
    {
    }

    public PublishedException(string name, object? key)
        : base($"{name} ({key}) is published")
    {
    }
}