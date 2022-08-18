using MassTransit;
using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;

namespace TwitterStreamAnalytics.Consumers.Application.Queries;

public interface IGetStatsFromCommandDbContext
{
}

public class GetStatsFromCommandDbContextConsumer : IConsumer<IGetStatsFromCommandDbContext>
{
    private readonly CommandDbContext _dbContext;

    public GetStatsFromCommandDbContextConsumer(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<IGetStatsFromCommandDbContext> context)
    {
        await context.RespondAsync<IStatsFromCommandDbContext>(new
        {
            TweetCount = await _dbContext.Tweets.CountAsync(context.CancellationToken),
            TopHashtags = await _dbContext.Hashtags
                .OrderByDescending(h => h.Count)
                .Take(10)
                .Select(h => new { h.Tag, h.Count })
                .ToListAsync(context.CancellationToken)
        });
    }
}

public interface IStatsFromCommandDbContext
{
    public int TweetCount { get; }
    public IReadOnlyList<IHashtag> TopHashtags { get; }
}

public interface IHashtag
{
    public string Tag { get; }
    public int Count { get; }
}