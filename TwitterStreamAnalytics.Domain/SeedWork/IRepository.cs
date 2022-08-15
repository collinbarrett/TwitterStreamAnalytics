namespace TwitterStreamAnalytics.Domain.SeedWork;

public interface IRepository<TAggregateRoot> : IUnitOfWork where TAggregateRoot : IAggregateRoot
{
}