using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Api.Application.Commands;
using TwitterStreamAnalytics.Api.Application.Queries;
using TwitterStreamAnalytics.Consumers.Application.Consumers;

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
                imbc.ConfigureEndpoints(context);

                // TODO: evaluate if retry policy is reasonable
                imbc.UseMessageRetry(r => r.Immediate(10));
            });
        });
        services.AddHostedService<BusIgnition>();
    }
}