using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

// TODO: mv to Consumers.Domain when separate DbContexts are possible
public class Hashtag : IAggregateRoot
{
    public Hashtag(string tag)
    {
        Tag = tag;
    }

    public string Tag { get; init; }
    public int Count { get; private set; } = 1;

    public void IncrementCount()
    {
        Count++;
    }
}