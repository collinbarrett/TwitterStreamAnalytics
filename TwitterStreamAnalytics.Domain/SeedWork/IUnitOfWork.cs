namespace TwitterStreamAnalytics.Domain.SeedWork;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}