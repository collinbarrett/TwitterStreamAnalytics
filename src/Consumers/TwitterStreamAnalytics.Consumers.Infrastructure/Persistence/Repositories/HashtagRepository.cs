using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.Repositories;

internal class HashtagRepository : IHashtagRepository
{
    private readonly CommandDbContext _dbContext;

    public HashtagRepository(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ValueTask<Hashtag?> FindAsync(string hashtag, CancellationToken cancellationToken = default)
    {
        return _dbContext.Hashtags.FindAsync(new object?[] { hashtag }, cancellationToken);
    }

    public void Add(Hashtag hashtag)
    {
        _dbContext.Add(hashtag);
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}