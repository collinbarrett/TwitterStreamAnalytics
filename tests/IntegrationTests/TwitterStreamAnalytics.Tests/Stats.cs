using TwitterStreamAnalytics.Api.Application.Queries;

namespace TwitterStreamAnalytics.Tests;

internal class Stats //: IStats
{
    public bool IsReadingStream { get; init; }
    public int TweetCount { get; init; }
    public int HashtagCount { get; init; }
    public List<Hashtag> TopHashtags { get; init; } = new();
}

internal class Hashtag : IHashtag
{
    public string Tag { get; init; } = default!;
    public int Count { get; init; }
}