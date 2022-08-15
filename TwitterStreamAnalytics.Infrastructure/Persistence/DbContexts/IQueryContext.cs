using TwitterStreamAnalytics.Domain.Aggregates;

namespace TwitterStreamAnalytics.Infrastructure.Persistence.DbContexts;

public interface IQueryContext
{
    IQueryable<Tweet> Tweets { get; }
    IQueryable<Hashtag> Hashtags { get; }
}