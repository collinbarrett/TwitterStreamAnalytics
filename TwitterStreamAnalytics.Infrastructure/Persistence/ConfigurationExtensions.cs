using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TwitterStreamAnalytics.Infrastructure.Persistence;

internal static class ConfigurationExtensions
{
    public static void AddPersistence(this IServiceCollection services)
    {
        //TODO: replace w/persistent db provider (not required for demo)
        services.AddDbContextPool<AnalyticsContext>(o => o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics)));
    }
}