namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence.Entities;

public record Tweet
{
    public string Id { get; init; } = default!;
}