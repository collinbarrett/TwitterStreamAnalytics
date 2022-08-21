using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Tweetinvi;
using Tweetinvi.Events.V2;
using Tweetinvi.Streaming.V2;
using TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Api.Infrastructure.Tests.TwitterClient;

public class TwitterStreamReaderTests
{
    private readonly ILogger<TwitterStreamReader> _logger = new NullLogger<TwitterStreamReader>();
    private readonly ITwitterStreamReader _sut;

    public TwitterStreamReaderTests()
    {
        var twitterClientMock = new Mock<ITwitterClient>();
        twitterClientMock.Setup(tc => tc.StreamsV2.CreateSampleStream()).Returns(Mock.Of<ISampleStreamV2>());
        _sut = new TwitterStreamReader(twitterClientMock.Object, _logger);
    }

    [Fact]
    public void Start_ReportIsReadingStream()
    {
        // Arrange

        // Act
        _sut.Start(It.IsAny<EventHandler<TweetV2ReceivedEventArgs>>());

        // Assert
        Assert.True(_sut.IsReadingStream);
    }

    [Fact]
    public void Stop_ReportIsNotReadingStream()
    {
        // Arrange

        // Act
        _sut.Stop();

        // Assert
        Assert.False(_sut.IsReadingStream);
    }
}