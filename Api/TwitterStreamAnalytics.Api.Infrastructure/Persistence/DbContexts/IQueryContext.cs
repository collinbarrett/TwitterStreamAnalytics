using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence.DbContexts;

public interface IQueryContext
{
    IQueryable<Tweet> Tweets { get; }
    IQueryable<Hashtag> Hashtags { get; }
}