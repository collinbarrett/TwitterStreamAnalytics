using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Api.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

namespace TwitterStreamAnalytics.Api.Infrastructure.Persistence;

internal static class ConfigurationExtensions
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContextPool<QueryDbContext>(o =>
        {
            // TODO: replace w/nonvolatile database
            o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics), MyInMemoryDatabase.Root);
        });
        services.AddScoped<IQueryContext, QueryContext>();
    }
}