using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.Consumers.Domain.Repositories;

public interface IHashtagRepository : IRepository<Hashtag>
{
    ValueTask<Hashtag?> FindAsync(string hashtag, CancellationToken cancellationToken);
    void Add(Hashtag hashtag);
}