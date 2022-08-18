namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence.Entities;

public record Hashtag
{
    public string Tag { get; init; } = default!;
    public int Count { get; init; }
}