using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

public class Tweet : IAggregateRoot
{
    public Tweet(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
}