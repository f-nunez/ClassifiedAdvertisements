namespace Ads.Command.Domain.Exceptions;

public class DeletedException : Exception
{
    public DeletedException() : base()
    {
    }

    public DeletedException(string message) : base(message)
    {
    }

    public DeletedException(string name, object? key)
        : base($"{name} ({key}) is deleted")
    {
    }
}