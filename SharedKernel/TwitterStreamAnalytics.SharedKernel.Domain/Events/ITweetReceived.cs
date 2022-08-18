namespace TwitterStreamAnalytics.SharedKernel.Domain.Events;

public interface ITweetReceived
{
    string Id { get; }

    // TODO: empty initialized instances of less derived interfaces (e.g. ICollection<T>) seem to get deserialized to null by MassTransit
    IList<string> Hashtags { get; }
}