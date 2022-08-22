using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

// TODO: do not share entity models between Api and Consumers (blocked by https://github.com/dotnet/efcore/issues/28778#issuecomment-1221324545)
public class Tweet : IAggregateRoot
{
    public Tweet(ulong id)
    {
        Id = id;
    }

    /// <summary>
    ///     https://developer.twitter.com/en/docs/twitter-ids
    /// </summary>
    public ulong Id { get; init; }
}