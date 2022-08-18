using Tweetinvi.Events.V2;

namespace TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;

/// <summary>
///     The singleton service persistent subscribing to the Twitter sample stream.
/// </summary>
/// <seealso cref="IDisposable" />
public interface ITwitterStreamReader : IDisposable
{
    void Start(EventHandler<TweetV2ReceivedEventArgs> onTweetReceived);
    void Stop();
}