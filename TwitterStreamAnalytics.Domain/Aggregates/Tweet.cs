using TwitterStreamAnalytics.Domain.SeedWork;

namespace TwitterStreamAnalytics.Domain.Aggregates;

public class Tweet : IAggregateRoot
{
    public Tweet(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
}