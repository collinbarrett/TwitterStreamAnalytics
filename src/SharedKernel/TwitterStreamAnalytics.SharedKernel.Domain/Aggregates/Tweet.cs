using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

// TODO: do not share entity models between Api and Consumers https://github.com/dotnet/efcore/issues/28778#issuecomment-1221324545
public class Tweet : IAggregateRoot
{
    public Tweet(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
}