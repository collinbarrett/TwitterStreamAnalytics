# TwitterStreamAnalytics
Analyzing the Twitter 1% Sample Stream in .NET

## Configuration

Add [Twitter App Bearer Token](https://developer.twitter.com/en/docs/authentication/oauth-2-0/bearer-tokens) as an [environment variable](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#environment-variables):

```
Twitter__AppBearerToken=
```

or [secret](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#secret-manager):

```
"Twitter:AppBearerToken": ""
```