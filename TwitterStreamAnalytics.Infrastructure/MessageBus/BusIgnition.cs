using MassTransit;
using Microsoft.Extensions.Hosting;

namespace TwitterStreamAnalytics.Infrastructure.MessageBus;

internal class BusIgnition : IHostedService
{
    private readonly IBusControl _busControl;

    public BusIgnition(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _busControl.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _busControl.StopAsync(cancellationToken);
    }
}