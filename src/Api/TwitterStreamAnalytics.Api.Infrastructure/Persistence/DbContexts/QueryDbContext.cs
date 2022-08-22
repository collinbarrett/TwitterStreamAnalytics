using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Api.Infrastructure.Persistence.EntityTypeConfigurations;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence.DbContexts;

internal class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {
    }

    // TODO: do not share entity models between Api and Consumers (blocked by https://github.com/dotnet/efcore/issues/28778#issuecomment-1221324545)
    public DbSet<Hashtag> Hashtags => Set<Hashtag>();
    public DbSet<Tweet> Tweets => Set<Tweet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            GetType().Assembly,
            type => type.Namespace == typeof(HashtagTypeConfiguration).Namespace);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}