using MassTransit;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Streaming.V2;
using TwitterStreamAnalytics.Domain.Events;

namespace TwitterStreamAnalytics.Infrastructure.TwitterClient;

public interface ITwitterStreamReader : IDisposable
{
    void Start();
    void Stop();
}

internal sealed class TwitterStreamReader : ITwitterStreamReader
{
    private readonly IBus _bus;
    private readonly ITwitterClient _client;
    private readonly ILogger<TwitterStreamReader> _logger;
    private ISampleStreamV2? _stream;

    public TwitterStreamReader(IBus bus, ITwitterClient twitterClient, ILogger<TwitterStreamReader> logger)
    {
        _bus = bus;
        _client = twitterClient;
        _logger = logger;
    }

    public void Start()
    {
        if (_stream != default) return;
        _stream = _client.StreamsV2.CreateSampleStream();
        _stream.TweetReceived += async (_, args) =>
        {
            await _bus.Publish<ITweetReceived>(new
                { args.Tweet.Id, Hashtags = args.Tweet.Entities.Hashtags?.Select(h => h.Tag) });
            _logger.LogInformation("Tweet ID {Id} received.", args.Tweet.Id);
        };
        Task.Run(() => _stream.StartAsync());
    }

    public void Stop()
    {
        if (_stream == default) return;
        _stream.StopStream();
        _stream = default;
    }

    public void Dispose()
    {
        Stop();
    }
}