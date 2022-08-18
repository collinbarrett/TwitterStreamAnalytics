using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence;

namespace TwitterStreamAnalytics.Consumers.Infrastructure;

public static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddPersistence();
    }
}