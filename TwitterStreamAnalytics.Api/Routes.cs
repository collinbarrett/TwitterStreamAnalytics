using MassTransit;
using MassTransit.Mediator;
using TwitterStreamAnalytics.Application.Analytics;
using TwitterStreamAnalytics.Application.StreamReader;

namespace TwitterStreamAnalytics.Api;

internal static class Routes
{
    public static void AddRoutes(this WebApplication app)
    {
        //TODO: refactor routes to RESTful PUT {status: started} or {status: stopped}
        app.MapPost("/start",
            async (IMediator mediator) =>
                await mediator.Send<IStartStreamReader>(new { })).WithName("Start");
        app.MapPost("/stop",
            async (IMediator mediator) =>
                await mediator.Send<IStopStreamReader>(new { })).WithName("Stop");

        app.MapGet("/stats",
            async (IRequestClient<IGetStats> mediator) =>
                await mediator.GetResponse<IStats>(new { })).WithName("Stats");
    }
}