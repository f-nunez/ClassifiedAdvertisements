namespace Ads.Command.Domain.Exceptions;

public class WhenException : Exception
{
    public WhenException(string eventType)
        : base($"When method not found the event for {eventType}")
    {
    }
}