using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tweetinvi;
using TwitterStreamAnalytics.Infrastructure.Persistence;
using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Infrastructure;

public static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TwitterOptions>(configuration.GetSection(TwitterOptions.Twitter));
        services.AddSingleton<ITwitterClient>(s =>
        {
            var twitterOptions = s.GetRequiredService<IOptions<TwitterOptions>>().Value;
            return new Tweetinvi.TwitterClient(default, default, twitterOptions.AppBearerToken);
        });
        services.AddSingleton<ITwitterStreamReader, TwitterStreamReader>();

        //TODO: replace w/persistent db provider (not required for demo)
        services.AddDbContextPool<AnalyticsContext>(o => o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics)));
    }
}