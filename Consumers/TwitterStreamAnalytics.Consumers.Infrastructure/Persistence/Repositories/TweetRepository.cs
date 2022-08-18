using TwitterStreamAnalytics.Consumers.Domain.Aggregates;
using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;

namespace TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.Repositories;

internal class TweetRepository : ITweetRepository
{
    private readonly CommandDbContext _dbContext;

    public TweetRepository(CommandDbContext dbContext)
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