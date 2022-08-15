using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TwitterStreamAnalytics.Infrastructure.MessageBus;

/// <summary>
///     The "ignition" worker of the MassTransit message bus.
/// </summary>
/// <seealso cref="IHostedService" />
internal class BusIgnition : IHostedService
{
    private readonly IBusControl _busControl;
    private readonly ILogger<BusIgnition> _logger;

    public BusIgnition(IBusControl busControl, ILogger<BusIgnition> logger)
    {
        _busControl = busControl;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting message bus.");
        return _busControl.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Stopping message bus.");
        return _busControl.StopAsync(cancellationToken);
    }
}