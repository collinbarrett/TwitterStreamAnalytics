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
        services.AddMediator(cfg =>
        {
            //TODO: register all automatically
            cfg.AddConsumer<StartStreamReaderConsumer>();
            cfg.AddConsumer<StopStreamReaderConsumer>();
            cfg.AddConsumer<GetStatsConsumer>();
            cfg.AddRequestClient<IGetStats>();
        });

        //TODO: move to infrastructure project?
        services.AddMassTransit(bc =>
        {
            //TODO: register all automatically
            bc.AddConsumer<AnalyzeTweet>();

            //TODO: replace w/alternate transport for persistence/scale (https://masstransit-project.com/usage/transports/)
            bc.UsingInMemory((context, imbc) => imbc.ConfigureEndpoints(context));
        });
    }
}