using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Application.StreamReader;
using TwitterStreamAnalytics.Infrastructure;

namespace TwitterStreamAnalytics.Application;

public static class ConfigurationExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddMediator(cfg => cfg.AddConsumersFromNamespaceContaining<StartStreamReader.Consumer>());
    }
}