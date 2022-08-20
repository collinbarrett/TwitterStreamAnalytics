using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

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

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}