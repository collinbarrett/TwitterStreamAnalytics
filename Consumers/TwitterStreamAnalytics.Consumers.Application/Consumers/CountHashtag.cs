using MassTransit;
using Microsoft.Extensions.Logging;
using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Domain.Events;

namespace TwitterStreamAnalytics.Consumers.Application.Consumers;

public class CountHashtag : IConsumer<IHashtagReceived>
{
    private readonly ILogger<CountHashtag> _logger;
    private readonly IHashtagRepository _repo;

    public CountHashtag(ILogger<CountHashtag> logger, IHashtagRepository hashtagRepository)
    {
        _logger = logger;
        _repo = hashtagRepository;
    }

    public async Task Consume(ConsumeContext<IHashtagReceived> context)
    {
        var hashtag = context.Message.Tag;
        var existingHashtag = await _repo.FindAsync(hashtag, context.CancellationToken);
        if (existingHashtag is null)
        {
            _repo.Add(new Hashtag(hashtag));
        }
        else
        {
            existingHashtag.IncrementCount();
        }

        try
        {
            await _repo.CommitAsync(context.CancellationToken);
            _logger.LogInformation("Counted occurrence #{Count} of hashtag {Hashtag}.",
                existingHashtag?.Count ?? 1,
                hashtag);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to count new occurrence of hashtag {Hashtag}.", hashtag);

            // TODO: configure message broker to retry on concurrency exceptions ArgumentException or DbUpdateConcurrencyException
            throw;
        }
    }
}