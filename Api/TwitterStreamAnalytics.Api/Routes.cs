using MassTransit;
using MassTransit.Mediator;
using TwitterStreamAnalytics.Api.Application.Commands;
using TwitterStreamAnalytics.Api.Application.Queries;

namespace TwitterStreamAnalytics.Api;

internal static class Routes
{
    // TODO: expose Swagger docs
    public static void AddRoutes(this WebApplication app)
    {
        //// TODO: refactor routes to RESTful PUT {status: started} or {status: stopped}
        app.MapPost("/start",
            (IMediator mediator) => mediator.Send<IStartStreamReader>(new { }));
        app.MapPost("/stop",
            (IMediator mediator) => mediator.Send<IStopStreamReader>(new { }));

        app.MapGet("/stats",
            (IRequestClient<IGetStats> mediator, CancellationToken cancellationToken) =>
                mediator.GetResponse<IStats>(new { }, cancellationToken));
    }
}