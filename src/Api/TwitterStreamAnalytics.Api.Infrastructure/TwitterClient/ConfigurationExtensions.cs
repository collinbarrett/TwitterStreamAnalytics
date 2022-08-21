using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tweetinvi;

namespace TwitterStreamAnalytics.Api.Infrastructure.TwitterClient;

internal static class ConfigurationExtensions
{
    public static void AddTwitterClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TwitterOptions>(configuration.GetSection(TwitterOptions.Twitter));
        services.AddSingleton<ITwitterClient>(s =>
            new Tweetinvi.TwitterClient(
                default,
                default,
                s.GetRequiredService<IOptions<TwitterOptions>>().Value.AppBearerToken));
        services.AddSingleton<ITwitterStreamReader, TwitterStreamReader>();
    }
}