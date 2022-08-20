using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.EntityTypeConfigurations;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;

public class CommandDbContext : DbContext
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
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