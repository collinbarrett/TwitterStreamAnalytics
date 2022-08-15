using MassTransit;
using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Application.Commands;

public interface IStartStreamReader
{
}

public class StartStreamReaderConsumer : IConsumer<IStartStreamReader>
{
    private readonly ITwitterStreamReader _streamReader;

    public StartStreamReaderConsumer(ITwitterStreamReader streamReader)
    {
        _streamReader = streamReader;
    }

    public Task Consume(ConsumeContext<IStartStreamReader> context)
    {
        _streamReader.Start();
        return Task.CompletedTask;
    }
}