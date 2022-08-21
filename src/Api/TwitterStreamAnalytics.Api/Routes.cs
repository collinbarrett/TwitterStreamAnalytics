using MassTransit;
using MassTransit.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using TwitterStreamAnalytics.Api.Application.Commands;
using TwitterStreamAnalytics.Api.Application.Queries;

namespace TwitterStreamAnalytics.Api;

internal static class Routes
{
    // TODO: expose Swagger docs in Swagger UI
    public static void AddRoutes(this WebApplication app)
    {
        // TODO: combine start/stop routes to RESTful PUT /status w/enum {status: started} or {status: stopped}

        app.MapPost("/start", [SwaggerOperation("Starts the Twitter stream reader.")]
            (IMediator mediator, CancellationToken cancellationToken) =>
                mediator.Send<IStartStreamReader>(new { }, cancellationToken));
        app.MapPost("/stop", [SwaggerOperation("Stops the Twitter stream reader.")]
            (IMediator mediator, CancellationToken cancellationToken) =>
                mediator.Send<IStopStreamReader>(new { }, cancellationToken));

        app.MapGet("/stats", [SwaggerOperation("Gets the tweet stats.")]
            (IRequestClient<IGetStats> mediator, CancellationToken cancellationToken) =>
                mediator.GetResponse<IStats>(new { }, cancellationToken));
    }
}