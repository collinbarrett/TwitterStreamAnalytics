using MassTransit;
using TwitterStreamAnalytics.Consumers.Domain.Aggregates;
using TwitterStreamAnalytics.Consumers.Domain.Repositories;

namespace TwitterStreamAnalytics.Consumers.Application.Commands;

public interface IAddSampleTweet
{
}

public class AddSampleTweet : IConsumer<IAddSampleTweet>
{
    private readonly ITweetRepository _repo;

    public AddSampleTweet(ITweetRepository tweetRepository)
    {
        _repo = tweetRepository;
    }

    public async Task Consume(ConsumeContext<IAddSampleTweet> context)
    {
        var tweet = new Tweet(new Random().Next(1, int.MaxValue).ToString());
        _repo.Add(tweet);
        await _repo.CommitAsync(context.CancellationToken);
    }
}