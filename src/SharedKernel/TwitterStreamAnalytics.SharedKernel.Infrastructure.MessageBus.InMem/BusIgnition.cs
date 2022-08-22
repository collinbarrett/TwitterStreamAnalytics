using System.Threading.Channels;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TwitterStreamAnalytics.SharedKernel.Infrastructure.MessageBus.InMem;

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

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Stopping message bus.");

        try
        {
            await _busControl.StopAsync(cancellationToken);
        }
        catch (ChannelClosedException ex)
        {
            // TODO: fix vs swallow ChannelClosedException on dispose during integration tests (ref https://github.com/MassTransit/MassTransit/discussions/3283)
            _logger.LogError(ex, $"{nameof(ChannelClosedException)} occurred on disposal of message bus.");
        }
    }
}