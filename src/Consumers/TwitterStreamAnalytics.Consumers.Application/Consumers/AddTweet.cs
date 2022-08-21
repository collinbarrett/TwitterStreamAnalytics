using MassTransit;
using Microsoft.Extensions.Logging;
using TwitterStreamAnalytics.Consumers.Domain.Repositories;
using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;
using TwitterStreamAnalytics.SharedKernel.Domain.Events;

namespace TwitterStreamAnalytics.Consumers.Application.Consumers;

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