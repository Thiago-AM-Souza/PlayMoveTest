using BuildingBlocks.DomainObjects;

namespace BuildingBlocks.Abstractions
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
