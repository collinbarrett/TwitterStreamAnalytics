using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Infrastructure.Persistence.Entities;

namespace TwitterStreamAnalytics.Infrastructure.Persistence;

public class AnalyticsContext : DbContext
{
    public AnalyticsContext(DbContextOptions<AnalyticsContext> options) : base(options)
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