using System.Runtime.Serialization;

namespace TwitterStreamAnalytics.Consumers.Application.Exceptions;

[Serializable]
public class ConcurrentHashtagIncrementException : Exception
{
    public ConcurrentHashtagIncrementException()
    {
    }

    public ConcurrentHashtagIncrementException(string message)
        : base(message)
    {
    }

    public ConcurrentHashtagIncrementException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected ConcurrentHashtagIncrementException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}