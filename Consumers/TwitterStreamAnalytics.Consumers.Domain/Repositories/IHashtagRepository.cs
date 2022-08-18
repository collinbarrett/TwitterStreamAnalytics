using TwitterStreamAnalytics.Consumers.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.Consumers.Domain.Repositories;

public interface IHashtagRepository : IRepository<Hashtag>
{
    Task<List<Hashtag>> FindAsync(IEnumerable<string> hashtags, CancellationToken cancellationToken);
    void AddRange(IEnumerable<Hashtag> hashtags);
}