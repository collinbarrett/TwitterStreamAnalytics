using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Domain.SeedWork;
using TwitterStreamAnalytics.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.Infrastructure.Persistence.Repositories;

namespace TwitterStreamAnalytics.Infrastructure.Persistence;

internal static class ConfigurationExtensions
{
    public static void AddPersistence(this IServiceCollection services)
    {
        //TODO: replace w/persistent db provider (not required for demo)
        services.AddDbContextPool<AnalyticsDbContext>(o => o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics)));

        services.AddScoped<IQueryContext, QueryContext>();

        services.Scan(s => s.FromAssemblyOf<TweetRepository>()
            .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}