using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence.Entities;

public class Tweet : IAggregateRoot
{
    public Tweet(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
}