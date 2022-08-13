using Tweetinvi;

namespace TwitterStreamAnalytics.Infrastructure;

public interface ITwitterStreamReader
{
    Task StartAsync();
}

public class TwitterStreamReader : ITwitterStreamReader
{
    private readonly ITwitterClient _twitterClient;

    public TwitterStreamReader(ITwitterClient twitterClient)
    {
        _twitterClient = twitterClient;
    }

    public async Task StartAsync()
    {
        var sampleStreamV2 = _twitterClient.StreamsV2.CreateSampleStream();
        sampleStreamV2.TweetReceived += (_, args) => Console.WriteLine(args.Tweet.Text);
        await sampleStreamV2.StartAsync();
    }
}