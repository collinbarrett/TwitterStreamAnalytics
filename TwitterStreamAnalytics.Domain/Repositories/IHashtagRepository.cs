using TwitterStreamAnalytics.Domain.Aggregates;
using TwitterStreamAnalytics.Domain.SeedWork;

namespace TwitterStreamAnalytics.Domain.Repositories;

public interface IHashtagRepository : IRepository<Hashtag>
{
    Task<List<Hashtag>> FindAsync(IEnumerable<string> hashtags, CancellationToken cancellationToken);
    void AddRange(IEnumerable<Hashtag> hashtags);
}