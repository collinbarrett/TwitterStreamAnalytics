using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Api.Infrastructure.Persistence.Entities;
using TwitterStreamAnalytics.Api.Infrastructure.Persistence.EntityTypeConfigurations;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence.DbContexts;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
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