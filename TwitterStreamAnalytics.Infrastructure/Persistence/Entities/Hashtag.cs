using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TwitterStreamAnalytics.Infrastructure.Persistence.Entities;

public class Hashtag
{
    public string Value { get; set; } = default!;
    public int Count { get; set; }
}

internal class HashtagTypeConfiguration : IEntityTypeConfiguration<Hashtag>
{
    public void Configure(EntityTypeBuilder<Hashtag> builder)
    {
        builder.HasKey(nameof(Hashtag.Value));
    }
}