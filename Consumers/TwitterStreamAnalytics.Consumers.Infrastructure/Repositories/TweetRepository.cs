using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

namespace TwitterStreamAnalytics.Consumers.Infrastructure.Repositories;

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