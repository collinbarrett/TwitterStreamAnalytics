namespace TwitterStreamAnalytics.Domain.Entities;

public class Tweet
{
    public Tweet(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
}