using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterStreamAnalytics.Domain.Aggregates;

namespace TwitterStreamAnalytics.Infrastructure.Persistence.EntityTypeConfigurations;

internal class HashtagTypeConfiguration : IEntityTypeConfiguration<Hashtag>
{
    public void Configure(EntityTypeBuilder<Hashtag> builder)
    {
        builder.HasKey(nameof(Hashtag.Tag));
        builder.Property(h => h.Count).IsConcurrencyToken();
    }
}