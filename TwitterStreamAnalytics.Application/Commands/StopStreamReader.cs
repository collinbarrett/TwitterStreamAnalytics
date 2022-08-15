using MassTransit;
using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Application.Commands;

public interface IStopStreamReader
{
}

public class StopStreamReaderConsumer : IConsumer<IStopStreamReader>
{
    private readonly ITwitterStreamReader _reader;

    public StopStreamReaderConsumer(ITwitterStreamReader twitterStreamReader)
    {
        _reader = twitterStreamReader;
    }

    public Task Consume(ConsumeContext<IStopStreamReader> context)
    {
        _reader.Stop();
        return Task.CompletedTask;
    }
}