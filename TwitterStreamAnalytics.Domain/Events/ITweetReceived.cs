namespace TwitterStreamAnalytics.Domain.Events;

public interface ITweetReceived
{
    string Id { get; }
    IEnumerable<string> Hashtags { get; }
}