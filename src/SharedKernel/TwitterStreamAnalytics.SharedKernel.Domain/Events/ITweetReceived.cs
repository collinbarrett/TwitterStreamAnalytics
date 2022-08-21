namespace TwitterStreamAnalytics.SharedKernel.Domain.Events;

public interface ITweetReceived
{
    string Id { get; }
}