using MassTransit.Mediator;
using TwitterStreamAnalytics.Application.StreamReader;

namespace TwitterStreamAnalytics.Api;

public static class Routes
{
    public static void AddRoutes(this WebApplication app)
    {
        //TODO: refactor routes to RESTful PUT {status: started} or {status: stopped}
        app.MapPost("/start",
            async (IMediator mediator) => await mediator.Send(new StartStreamReader.Request())).WithName("Start");
        app.MapPost("/stop",
            async (IMediator mediator) => await mediator.Send(new StopStreamReader.Request())).WithName("Stop");
    }
}