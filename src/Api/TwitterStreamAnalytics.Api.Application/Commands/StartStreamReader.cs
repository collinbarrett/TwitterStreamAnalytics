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
                Id = ulong.Parse(args.Tweet.Id)
            });
            if (args.Tweet.Entities.Hashtags is null) return;
            foreach (var hashtag in args.Tweet.Entities.Hashtags)
            {
                _bus.Publish<IHashtagReceived>(new
                {
                    hashtag.Tag
                });
            }
        });
        return Task.CompletedTask;
    }
}