namespace TwitterStreamAnalytics.Infrastructure;

internal class TwitterOptions
{
    public const string Twitter = "Twitter";

    public string AppBearerToken { get; set; } = string.Empty;
}