using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence;

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