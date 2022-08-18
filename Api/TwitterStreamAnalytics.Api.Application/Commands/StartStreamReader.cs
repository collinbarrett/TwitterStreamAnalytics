using MassTransit;
using TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;
using TwitterStreamAnalytics.SharedKernel.Domain.Events;

namespace TwitterStreamAnalytics.Api.Application.Commands;

public interface IStartStreamReader
{
}

public class StartStreamReaderConsumer : IConsumer<IStartStreamReader>
{
    private readonly IBus _bus;
    private readonly ITwitterStreamReader _reader;

    public StartStreamReaderConsumer(IBus bus, ITwitterStreamReader twitterStreamReader)
    {
        _bus = bus;
        _reader = twitterStreamReader;
    }

    public Task Consume(ConsumeContext<IStartStreamReader> context)
    {
        _reader.Start((_, args) =>
        {
            _bus.Publish<ITweetReceived>(new
            {
                args.Tweet.Id,
                Hashtags = args.Tweet.Entities.Hashtags?.Select(h => h.Tag) ?? new List<string>()
            });
        });
        return Task.CompletedTask;
    }
}