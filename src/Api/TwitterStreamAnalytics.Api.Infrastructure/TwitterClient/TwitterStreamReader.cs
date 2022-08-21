using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Events.V2;
using Tweetinvi.Streaming.V2;

[assembly: InternalsVisibleTo("TwitterStreamAnalytics.Api.Infrastructure.Tests")]

namespace TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;

internal sealed class TwitterStreamReader : ITwitterStreamReader
{
    private readonly ITwitterClient _client;
    private readonly ILogger<TwitterStreamReader> _logger;
    private ISampleStreamV2? _stream;

    public TwitterStreamReader(ITwitterClient twitterClient, ILogger<TwitterStreamReader> logger)
    {
        _client = twitterClient;
        _logger = logger;
    }

    public bool IsReadingStream => _stream != default;

    public void Start(EventHandler<TweetV2ReceivedEventArgs> onTweetReceived)
    {
        if (IsReadingStream)
        {
            _logger.LogInformation($"{nameof(ITwitterStreamReader)} already started.");
            return;
        }

        _stream = _client.StreamsV2.CreateSampleStream();
        _stream.TweetReceived += onTweetReceived;
        Task.Run(() => _stream.StartAsync());
        _logger.LogInformation($"{nameof(ITwitterStreamReader)} started.");
    }

    public void Stop()
    {
        if (!IsReadingStream)
        {
            _logger.LogInformation($"{nameof(ITwitterStreamReader)} already stopped.");
            return;
        }

        _stream?.StopStream();
        _stream = default;
        _logger.LogInformation($"{nameof(ITwitterStreamReader)} already stopped.");
    }

    public void Dispose()
    {
        Stop();
        _logger.LogDebug($"{nameof(ITwitterStreamReader)} disposed.");
    }
}