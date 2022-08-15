using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Domain.Aggregates;

namespace TwitterStreamAnalytics.Infrastructure.Persistence.DbContexts;

internal class QueryContext : IQueryContext
{
    private readonly AnalyticsDbContext _dbContext;

    public QueryContext(AnalyticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Tweet> Tweets => _dbContext.Tweets.AsNoTracking();
    public IQueryable<Hashtag> Hashtags => _dbContext.Hashtags.AsNoTracking();
}