using System.Runtime.Serialization;

namespace TwitterStreamAnalytics.Consumers.Application.Exceptions;

[Serializable]
public class ConcurrentHashtagAddException : Exception
{
    public ConcurrentHashtagAddException()
    {
    }

    public ConcurrentHashtagAddException(string message)
        : base(message)
    {
    }

    public ConcurrentHashtagAddException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected ConcurrentHashtagAddException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}