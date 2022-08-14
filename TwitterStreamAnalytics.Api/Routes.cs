using TwitterStreamAnalytics.Infrastructure.TwitterClient;

namespace TwitterStreamAnalytics.Api;

public static class Routes
{
    public static void AddRoutes(this WebApplication app)
    {
        //TODO: refactor routes to RESTful PUT {status: started} or {status: stopped}
        app.MapPost("/start",
            (ITwitterStreamReader streamReader) => streamReader.Start()).WithName("Start");
        app.MapPost("/stop",
            (ITwitterStreamReader streamReader) => streamReader.Stop()).WithName("Stop");
    }
}