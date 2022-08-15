using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Domain.Aggregates;
using TwitterStreamAnalytics.Infrastructure.Persistence.EntityTypeConfigurations;

namespace TwitterStreamAnalytics.Infrastructure.Persistence.DbContexts;

internal class AnalyticsDbContext : DbContext
{
    public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : base(options)
    {
    }

    public DbSet<Hashtag> Hashtags => Set<Hashtag>();
    public DbSet<Tweet> Tweets => Set<Tweet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            GetType().Assembly,
            type => type.Namespace == typeof(HashtagTypeConfiguration).Namespace);
    }
}