using Microsoft.Extensions.DependencyInjection;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence;

internal static class ConfigurationExtensions
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IQueryContext, QueryContext>();
    }
}