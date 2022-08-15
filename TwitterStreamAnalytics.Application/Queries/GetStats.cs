using MassTransit;
using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Infrastructure.Persistence;

namespace TwitterStreamAnalytics.Application.Queries;

public interface IGetStats
{
}

public class GetStatsConsumer : IConsumer<IGetStats>
{
    private readonly AnalyticsContext _dbContext;

    public GetStatsConsumer(AnalyticsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<IGetStats> context)
    {
        await context.RespondAsync<IStats>(new
        {
            TweetCount = await _dbContext.Tweets.CountAsync(),
            TopHashtags = await _dbContext.Hashtags
                .OrderByDescending(h => h.Count)
                .Take(10)
                .Select(h => new { h.Value, h.Count })
                .ToListAsync()
        });
    }
}

public interface IStats
{
    public int TweetCount { get; }
    public IEnumerable<IHashtag> TopHashtags { get; }
}

public interface IHashtag
{
    public string Value { get; }
    public int Count { get; }
}