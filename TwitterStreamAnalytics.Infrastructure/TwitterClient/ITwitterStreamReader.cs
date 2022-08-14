using Tweetinvi;
using Tweetinvi.Streaming.V2;

namespace TwitterStreamAnalytics.Infrastructure.TwitterClient;

public interface ITwitterStreamReader : IDisposable
{
    void Start();
    void Stop();
}

internal sealed class TwitterStreamReader : ITwitterStreamReader
{
    private readonly ITwitterClient _client;
    private ISampleStreamV2? _stream;

    public TwitterStreamReader(ITwitterClient twitterClient)
    {
        _client = twitterClient;
    }

    public void Start()
    {
        if (_stream != default) return;
        _stream = _client.StreamsV2.CreateSampleStream();
        _stream.TweetReceived += (_, args) => Console.WriteLine(args.Tweet.Text);
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