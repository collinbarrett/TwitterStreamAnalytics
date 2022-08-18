using Microsoft.Extensions.DependencyInjection;
using TwitterStreamAnalytics.Consumers.Infrastructure.Repositories;
using TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

namespace TwitterStreamAnalytics.Consumers.Infrastructure;

public static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.Scan(s => s.FromAssemblyOf<TweetRepository>()
            .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}