using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

// TODO: mv to Consumers.Domain when separate DbContexts are possible
public class Tweet : IAggregateRoot
{
    public Tweet(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
}