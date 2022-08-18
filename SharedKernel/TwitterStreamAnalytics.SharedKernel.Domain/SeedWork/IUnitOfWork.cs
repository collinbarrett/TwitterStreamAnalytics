namespace TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}