using MassTransit;
using Microsoft.Extensions.Logging;
using TwitterStreamAnalytics.Domain.Aggregates;
using TwitterStreamAnalytics.Domain.Events;
using TwitterStreamAnalytics.Domain.Repositories;

namespace TwitterStreamAnalytics.Application.EventConsumers;

public class AddTweet : IConsumer<ITweetReceived>
{
    private readonly ILogger<AddTweet> _logger;
    private readonly ITweetRepository _repo;

    public AddTweet(ILogger<AddTweet> logger, ITweetRepository tweetRepository)
    {
        _logger = logger;
        _repo = tweetRepository;
    }

    public async Task Consume(ConsumeContext<ITweetReceived> context)
    {
        var tweetId = context.Message.Id;
        _repo.Add(new Tweet(tweetId));
        await _repo.CommitAsync(context.CancellationToken);
        _logger.LogInformation("Tweet {Id} added.", tweetId);
    }
}