namespace TwitterStreamAnalytics.Domain.Entities;

public class Hashtag
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