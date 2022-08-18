using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.Repositories;
using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;
using TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

namespace TwitterStreamAnalytics.Consumers.Infrastructure.Persistence;

public static class ConfigurationExtensions
{
    public static void AddPersistence(this IServiceCollection services)
    {
        //services.AddDbContextPool<CommandDbContext>(o =>
        //{
        //    // TODO: replace w/nonvolatile database
        //    o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics), MyInMemoryDatabase.Root);
        //    //o.UseInternalServiceProvider(serviceProvider);
        //});
        services.Scan(s => s.FromAssemblyOf<TweetRepository>()
            .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}