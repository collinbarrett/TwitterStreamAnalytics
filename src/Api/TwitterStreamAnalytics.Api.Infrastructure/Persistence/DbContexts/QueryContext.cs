using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence.DbContexts;

internal class QueryContext : IQueryContext
{
    private readonly QueryDbContext _dbContext;

    public QueryContext(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Tweet> Tweets => _dbContext.Tweets.AsNoTracking();
    public IQueryable<Hashtag> Hashtags => _dbContext.Hashtags.AsNoTracking();
}