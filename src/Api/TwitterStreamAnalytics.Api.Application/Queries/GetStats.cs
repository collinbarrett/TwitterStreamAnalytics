using MassTransit;
using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Api.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Api.Application.Queries;

public interface IGetStats
{
}

public class GetStatsConsumer : IConsumer<IGetStats>
{
    private readonly IQueryContext _dbContext;
    private readonly ITwitterStreamReader _streamReader;

    public GetStatsConsumer(IQueryContext dbContext, ITwitterStreamReader streamReader)
    {
        _dbContext = dbContext;
        _streamReader = streamReader;
    }

    public async Task Consume(ConsumeContext<IGetStats> context)
    {
        var tweetCountAsync = _dbContext.Tweets.CountAsync(context.CancellationToken);
        var hashtagCountAsync = _dbContext.Hashtags.CountAsync(context.CancellationToken);
        var topHashtagsAsync = _dbContext.Hashtags
            .OrderByDescending(h => h.Count)
            .Take(10)
            .Select(h => new { h.Tag, h.Count })
            .ToListAsync(context.CancellationToken);
        await context.RespondAsync<IStats>(new
        {
            _streamReader.IsReadingStream,
            TweetCount = await tweetCountAsync,
            HashtagCount = await hashtagCountAsync,
            TopHashtags = await topHashtagsAsync
        });
    }
}

public interface IStats
{
    public bool IsReadingStream { get; }
    public int TweetCount { get; }
    public int HashtagCount { get; }
    public IReadOnlyList<IHashtag> TopHashtags { get; }
}

public interface IHashtag
{
    public string Tag { get; }
    public int Count { get; }
}