using TwitterStreamAnalytics.Domain.Aggregates;
using TwitterStreamAnalytics.Domain.SeedWork;

namespace TwitterStreamAnalytics.Domain.Repositories;

public interface ITweetRepository : IRepository<Tweet>
{
    void Add(Tweet tweet);
}