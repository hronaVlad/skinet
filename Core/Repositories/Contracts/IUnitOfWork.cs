using EFModels.Entities;

namespace Core.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : EntityKey;
        Task<int> Complete();
    }
}