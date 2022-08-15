using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tweetinvi.Core.Extensions;
using TwitterStreamAnalytics.Domain.Events;
using TwitterStreamAnalytics.Infrastructure.Persistence;
using TwitterStreamAnalytics.Infrastructure.Persistence.Entities;

namespace TwitterStreamAnalytics.Application.EventConsumers;

public class AnalyzeTweet : IConsumer<ITweetReceived>
{
    private readonly AnalyticsContext _dbContext;
    private readonly ILogger<AnalyzeTweet> _logger;

    public AnalyzeTweet(AnalyticsContext dbContext, ILogger<AnalyzeTweet> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ITweetReceived> context)
    {
        AddTweet(context.Message);
        await AddHashtags(context.Message);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Tweet analyzed: {Id}", context.Message.Id);
    }

    private void AddTweet(ITweetReceived tweet)
    {
        _dbContext.Tweets.Add(new Tweet
        {
            Id = tweet.Id
        });
    }

    private async Task AddHashtags(ITweetReceived tweet)
    {
        if (!tweet.Hashtags.IsNullOrEmpty())
        {
            var existingHashtags = await _dbContext.Hashtags
                .Where(h => tweet.Hashtags.Contains(h.Value))
                .ToListAsync();
            foreach (var hashtag in existingHashtags) hashtag.Count++;

            var newHashtags = tweet.Hashtags.ExceptBy(existingHashtags.Select(h => h.Value), h => h);
            _dbContext.Hashtags.AddRange(newHashtags.Select(h => new Hashtag
            {
                Value = h,
                Count = 1
            }));
        }
    }
}