using MassTransit;
using TwitterStreamAnalytics.Domain.Events;
using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Application.Commands;

public interface IStartStreamReader
{
}

public class StartStreamReaderConsumer : IConsumer<IStartStreamReader>
{
    private readonly IBus _bus;
    private readonly ITwitterStreamReader _streamReader;

    public StartStreamReaderConsumer(IBus bus, ITwitterStreamReader streamReader)
    {
        _streamReader = streamReader;
        _bus = bus;
    }

    public Task Consume(ConsumeContext<IStartStreamReader> context)
    {
        _streamReader.Start((_, args) =>
        {
            _bus.Publish<ITweetReceived>(new
            {
                args.Tweet.Id,
                Hashtags = args.Tweet.Entities.Hashtags?.Select(h => h.Tag)
            });
        });
        return Task.CompletedTask;
    }
}