using MassTransit;
using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Application.Commands;

public interface IStopStreamReader
{
}

public class StopStreamReaderConsumer : IConsumer<IStopStreamReader>
{
    private readonly ITwitterStreamReader _streamReader;

    public StopStreamReaderConsumer(ITwitterStreamReader streamReader)
    {
        _streamReader = streamReader;
    }

    public Task Consume(ConsumeContext<IStopStreamReader> context)
    {
        _streamReader.Stop();
        return Task.CompletedTask;
    }
}