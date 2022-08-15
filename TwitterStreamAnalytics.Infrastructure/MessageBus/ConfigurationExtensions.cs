using Microsoft.Extensions.DependencyInjection;

namespace TwitterStreamAnalytics.Infrastructure.MessageBus;

internal static class ConfigurationExtensions
{
    public static void AddMessageBus(this IServiceCollection services)
    {
        services.AddHostedService<BusIgnition>();
    }
}