using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

internal class HashtagTypeConfiguration : IEntityTypeConfiguration<Hashtag>
{
    public void Configure(EntityTypeBuilder<Hashtag> builder)
    {
        builder.HasKey(nameof(Hashtag.Tag));
        builder.Property(h => h.Count).IsConcurrencyToken();
    }
}