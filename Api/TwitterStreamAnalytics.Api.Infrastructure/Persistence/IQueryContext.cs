using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence;

public interface IQueryContext
{
    IQueryable<Tweet> Tweets { get; }
    IQueryable<Hashtag> Hashtags { get; }
}