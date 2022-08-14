using MassTransit;
using Microsoft.Extensions.Logging;
using TwitterStreamAnalytics.Domain.Events;

namespace TwitterStreamAnalytics.Application.Analytics;

public class AnalyzeTweet : IConsumer<ITweetReceived>
{
    private readonly ILogger<AnalyzeTweet> _logger;

    public AnalyzeTweet(ILogger<AnalyzeTweet> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ITweetReceived> context)
    {
        _logger.LogInformation("Tweet received: {Id}", context.Message.Id);
        return Task.CompletedTask;
    }
}