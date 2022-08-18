using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

public static class ConfigurationExtensions
{
    public static void AddInMemoryPersistence(this IServiceCollection services)
    {
        // TODO: do not share DbContext and Aggregates between API and Consumers (blocked by https://stackoverflow.com/questions/48437528/multi-context-inmemory-database#comment129607804_57602635)
        services.AddDbContextPool<AnalyticsDbContext>(o => o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics)));
    }
}