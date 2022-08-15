using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Application.Commands;
using TwitterStreamAnalytics.Application.EventConsumers;
using TwitterStreamAnalytics.Application.Queries;
using TwitterStreamAnalytics.Infrastructure;

namespace TwitterStreamAnalytics.Application;

public static class ConfigurationExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddMediator(cfg =>
        {
            cfg.AddConsumersFromNamespaceContaining<StartStreamReaderConsumer>();
            cfg.AddConsumersFromNamespaceContaining<GetStatsConsumer>();
            cfg.AddRequestClient<IGetStats>();
        });

        services.AddMassTransit(bc =>
        {
            bc.AddConsumersFromNamespaceContaining<AddTweet>();

            //TODO: replace w/alternate transport for persistence/scale (https://masstransit-project.com/usage/transports/)
            bc.UsingInMemory((context, imbc) => imbc.ConfigureEndpoints(context));
        });
    }
}