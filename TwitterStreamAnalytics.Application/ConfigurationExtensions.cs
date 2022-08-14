using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Application.Analytics;
using TwitterStreamAnalytics.Application.StreamReader;
using TwitterStreamAnalytics.Infrastructure;

namespace TwitterStreamAnalytics.Application;

public static class ConfigurationExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddMediator(cfg => cfg.AddConsumersFromNamespaceContaining<StartStreamReader.Consumer>());

        //TODO: move to infrastructure project?
        services.AddMassTransit(bc =>
        {
            bc.AddConsumersFromNamespaceContaining<AnalyzeTweet>();

            //TODO: replace w/alternate transport for persistence/scale (https://masstransit-project.com/usage/transports/)
            bc.UsingInMemory((context, imbc) => imbc.ConfigureEndpoints(context));
        });
    }
}