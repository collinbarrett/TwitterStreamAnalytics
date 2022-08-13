using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tweetinvi;

namespace TwitterStreamAnalytics.Infrastructure;

public static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TwitterOptions>(configuration.GetSection(TwitterOptions.Twitter));
        services.AddSingleton<ITwitterClient>(s =>
        {
            var twitterOptions = s.GetRequiredService<IOptions<TwitterOptions>>().Value;
            return new TwitterClient(default, default, twitterOptions.AppBearerToken);
        });
        services.AddSingleton<ITwitterStreamReader, TwitterStreamReader>();
    }
}