using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.Repositories;

internal class HashtagRepository : IHashtagRepository
{
    private readonly CommandDbContext _dbContext;
    private readonly ILogger<HashtagRepository> _logger;

    public HashtagRepository(CommandDbContext dbContext, ILogger<HashtagRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task<List<Hashtag>> FindAsync(IEnumerable<string> hashtags, CancellationToken cancellationToken)
    {
        return _dbContext.Hashtags.Where(h => hashtags.Contains(h.Tag)).ToListAsync(cancellationToken);
    }

    public void AddRange(IEnumerable<Hashtag> hashtags)
    {
        _dbContext.Hashtags.AddRange(hashtags);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        var saved = false;
        while (!saved)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                saved = true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is not Hashtag) continue;

                    // TODO: unit test concurrency conflict handling
                    var databaseValues = await entry.GetDatabaseValuesAsync(cancellationToken);

                    // TODO: use Hashtag.IncrementCount() method to re-increment
                    // re-increment Hashtag.Count from db latest
                    entry.CurrentValues[nameof(Hashtag.Count)] = (int)databaseValues?[nameof(Hashtag.Count)]! + 1;

                    // refresh original values to bypass next concurrency check
                    entry.OriginalValues.SetValues(databaseValues);
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Failed to count new hashtags.", ex);
                // TODO: handle concurrency conflicts on AddRange() ("An item with the same key has already been added".)
                saved = true;
            }
        }
    }
}