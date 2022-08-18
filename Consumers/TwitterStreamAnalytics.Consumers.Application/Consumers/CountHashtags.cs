using MassTransit;
using Microsoft.Extensions.Logging;
using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Domain.Events;

namespace TwitterStreamAnalytics.Consumers.Application.Consumers;

public class CountHashtags : IConsumer<ITweetReceived>
{
    private readonly ILogger<CountHashtags> _logger;
    private readonly IHashtagRepository _repo;

    public CountHashtags(ILogger<CountHashtags> logger, IHashtagRepository hashtagRepository)
    {
        _logger = logger;
        _repo = hashtagRepository;
    }

    public async Task Consume(ConsumeContext<ITweetReceived> context)
    {
        var tweet = context.Message;
        if (tweet.Hashtags.Count == 0)
        {
            _logger.LogInformation("No hashtags for tweet {Id} to count.", context.Message.Id);
        }
        else
        {
            var existingHashtags = await _repo.FindAsync(tweet.Hashtags, context.CancellationToken);
            foreach (var hashtag in existingHashtags) hashtag.IncrementCount();

            var newHashtags = tweet.Hashtags
                .ExceptBy(existingHashtags.Select(h => h.Tag), h => h)
                .Select(h => new Hashtag(h));
            _repo.AddRange(newHashtags);

            await _repo.CommitAsync(context.CancellationToken);
            _logger.LogInformation("Hashtags for tweet {Id} counted.", context.Message.Id);
        }
    }
}