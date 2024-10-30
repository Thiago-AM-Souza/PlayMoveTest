namespace BuildingBlocks.Abstractions
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
