using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.Consumers.Domain.Repositories;

public interface ITweetRepository : IRepository<Tweet>
{
    void Add(Tweet tweet);
}