using Tweetinvi.Events.V2;

namespace TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;

/// <summary>
///     The singleton Twitter sample stream subscriber.
/// </summary>
public interface ITwitterStreamReader : IDisposable
{
    bool IsReadingStream { get; }
    void Start(EventHandler<TweetV2ReceivedEventArgs> onTweetReceived);
    void Stop();
}