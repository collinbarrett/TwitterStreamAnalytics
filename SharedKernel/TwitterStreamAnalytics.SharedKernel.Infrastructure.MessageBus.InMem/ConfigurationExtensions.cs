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
            cfg.AddConsumersFromNamespaceContaining<GetStatsConsumer>();
            cfg.AddRequestClient<IGetStats>();
        });
        services.AddMassTransit(bc =>
        {
            bc.AddConsumersFromNamespaceContaining<AddTweet>();
            bc.UsingInMemory((context, imbc) => imbc.ConfigureEndpoints(context));
        });
        services.AddHostedService<BusIgnition>();
    }
}