using TwitterStreamAnalytics.Domain.Aggregates;
using TwitterStreamAnalytics.Domain.Repositories;
using TwitterStreamAnalytics.Infrastructure.Persistence.DbContexts;

namespace TwitterStreamAnalytics.Infrastructure.Persistence.Repositories;

internal class TweetRepository : ITweetRepository
{
    private readonly AnalyticsDbContext _dbContext;

    public TweetRepository(AnalyticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Tweet tweet)
    {
        _dbContext.Tweets.Add(tweet);
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}