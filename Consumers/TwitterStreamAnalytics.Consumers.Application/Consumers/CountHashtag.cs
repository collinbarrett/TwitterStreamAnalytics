using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TwitterStreamAnalytics.Consumers.Application.Exceptions;
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
        LogRetryAttempt();
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
            _logger.LogWarning(ex, "Failed to count new occurrence of hashtag {Hashtag}.", hashtag);
            switch (ex)
            {
                // message broker configured w/retry policy for custom concurrent exceptions
                case ArgumentException:
                    // when a new hashtag has already been added by another consumer instance
                    throw new ConcurrentHashtagAddException(ex.Message, ex);
                case DbUpdateConcurrencyException:
                    // when an existing hashtag has had its count incremented by another consumer instance
                    throw new ConcurrentHashtagIncrementException(ex.Message, ex);

                default:
                    throw;
            }
        }

        void LogRetryAttempt()
        {
            var retryAttempt = context.GetRetryAttempt();
            if (retryAttempt > 0)
            {
                _logger.LogInformation("Retry {RetryAttempt} to count new occurrence of hashtag {Hashtag}.",
                    retryAttempt,
                    hashtag);
            }
        }
    }
}