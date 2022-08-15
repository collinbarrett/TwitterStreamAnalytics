using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Infrastructure.MessageBus;
using TwitterStreamAnalytics.Infrastructure.Persistence;
using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Infrastructure;

public static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTwitterClient(configuration);
        services.AddMessageBus();
        services.AddPersistence();
    }
}