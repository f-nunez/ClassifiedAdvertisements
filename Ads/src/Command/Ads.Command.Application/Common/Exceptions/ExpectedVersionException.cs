namespace Ads.Command.Application.Common.Exceptions;

public class ExpectedVersionException : Exception
{
    public ExpectedVersionException()
        : base()
    {
    }

    public ExpectedVersionException(string message)
        : base(message)
    {
    }

    public ExpectedVersionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ExpectedVersionException(string streamName, long? expectedVersion, long? actualVersion)
        : base($"Append failed due to expected version. Stream: {streamName}, Expected version: {expectedVersion}, Actual version: {actualVersion}")
    {
    }
}