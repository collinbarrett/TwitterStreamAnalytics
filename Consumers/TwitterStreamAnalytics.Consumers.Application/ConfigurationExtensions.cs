using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Consumers.Infrastructure;

namespace TwitterStreamAnalytics.Consumers.Application;

public static class ConfigurationExtensions
{
    public static void AddConsumersApplication(this IServiceCollection services)
    {
        services.AddInfrastructure();
    }
}