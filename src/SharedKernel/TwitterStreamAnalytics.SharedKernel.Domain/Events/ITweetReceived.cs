namespace TwitterStreamAnalytics.SharedKernel.Domain.Events;

public interface ITweetReceived
{
    ulong Id { get; }
}