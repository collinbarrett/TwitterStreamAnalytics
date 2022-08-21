using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Api.Infrastructure.Persistence;
using TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Api.Infrastructure;

public static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTwitterClient(configuration);
        services.AddPersistence();
    }
}