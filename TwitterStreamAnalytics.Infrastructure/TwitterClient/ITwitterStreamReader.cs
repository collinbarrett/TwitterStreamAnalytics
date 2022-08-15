using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Events.V2;
using Tweetinvi.Streaming.V2;

namespace TwitterStreamAnalytics.Infrastructure.TwitterClient;

/// <summary>
///     The singleton service persistent subscribing to the Twitter sample stream.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ITwitterStreamReader : IDisposable
{
    void Start(EventHandler<TweetV2ReceivedEventArgs> onTweetReceived);
    void Stop();
}

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

    public void Start(EventHandler<TweetV2ReceivedEventArgs> onTweetReceived)
    {
        if (_stream != default)
        {
            _logger.LogInformation("StreamReader already started.");
            return;
        }

        _stream = _client.StreamsV2.CreateSampleStream();
        _stream.TweetReceived += onTweetReceived;
        Task.Run(() => _stream.StartAsync());
        _logger.LogInformation("StreamReader started.");
    }

    public void Stop()
    {
        if (_stream == default)
        {
            _logger.LogInformation("StreamReader already stopped.");
            return;
        }

        _stream.StopStream();
        _stream = default;
        _logger.LogInformation("StreamReader stopped.");
    }

    public void Dispose()
    {
        Stop();
        _logger.LogDebug("StreamReader disposed.");
    }
}