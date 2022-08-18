namespace TwitterStreamAnalytics.SharedKernel.Domain.SeedWork;

public interface IRepository<TAggregateRoot> : IUnitOfWork where TAggregateRoot : IAggregateRoot
{
}