using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Api.Infrastructure;

namespace TwitterStreamAnalytics.Api.Application;

public static class ConfigurationExtensions
{
    public static void AddApiApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
    }
}