using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.EntityTypeConfigurations;

internal class HashtagTypeConfiguration : IEntityTypeConfiguration<Hashtag>
{
    public void Configure(EntityTypeBuilder<Hashtag> builder)
    {
        builder.HasKey(nameof(Hashtag.Tag));

        // use Count as concurrency token w/message broker retry to ensure accurate count by parallel consumers
        builder.Property(h => h.Count).IsConcurrencyToken();
    }
}