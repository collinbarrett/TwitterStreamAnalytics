using MassTransit;
using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Application.StreamReader;

public static class StartStreamReader
{
    public record Request
    {
    }

    public class Consumer : IConsumer<Request>
    {
        private readonly ITwitterStreamReader _streamReader;

        public Consumer(ITwitterStreamReader streamReader)
        {
            _streamReader = streamReader;
        }

        public async Task Consume(ConsumeContext<Request> context)
        {
            _streamReader.Start();
            await context.RespondAsync(new Response());
        }
    }

    public record Response
    {
    }
}