using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

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