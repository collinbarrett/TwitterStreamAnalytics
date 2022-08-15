using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterStreamAnalytics.Domain.Entities;

namespace TwitterStreamAnalytics.Infrastructure.Persistence;

internal class HashtagTypeConfiguration : IEntityTypeConfiguration<Hashtag>
{
    public void Configure(EntityTypeBuilder<Hashtag> builder)
    {
        builder.HasKey(nameof(Hashtag.Tag));
    }
}