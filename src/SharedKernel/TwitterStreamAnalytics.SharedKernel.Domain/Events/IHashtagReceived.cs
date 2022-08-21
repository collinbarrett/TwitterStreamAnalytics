namespace TwitterStreamAnalytics.SharedKernel.Domain.Events;

public interface IHashtagReceived
{
    string Tag { get; }
}