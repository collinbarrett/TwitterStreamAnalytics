using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Api.Application.Commands;
using TwitterStreamAnalytics.Api.Application.Queries;
using TwitterStreamAnalytics.Consumers.Application.Consumers;
using TwitterStreamAnalytics.Consumers.Application.Exceptions;

namespace TwitterStreamAnalytics.SharedKernel.Infrastructure.MessageBus.InMem;

public static class ConfigurationExtensions
{
    public static void AddInMemoryMessageBus(this IServiceCollection services)
    {
        services.AddMediator(cfg =>
        {
            cfg.AddConsumersFromNamespaceContaining<StartStreamReaderConsumer>();
            cfg.AddRequestClient<IGetStats>();
            cfg.AddConsumersFromNamespaceContaining<GetStatsConsumer>();
        });
        services.AddMassTransit(bc =>
        {
            bc.AddConsumersFromNamespaceContaining<AddTweet>();
            bc.UsingInMemory((context, imbc) =>
            {
                imbc.ReceiveEndpoint("add-tweet", e => e.ConfigureConsumer<AddTweet>(context));
                imbc.ReceiveEndpoint("count-hashtag", e =>
                {
                    e.ConfigureConsumer<CountHashtag>(context);
                    e.UseMessageRetry(r =>
                    {
                        r.Handle<ConcurrentHashtagAddException>();
                        r.Handle<ConcurrentHashtagIncrementException>();
                        r.Immediate(5);
                    });
                });
            });
        });
        services.AddHostedService<BusIgnition>();
    }
}