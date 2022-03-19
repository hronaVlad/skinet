using EFModels.Entities;

namespace EFModels.Contracts
{
    public interface IGenericRepository<T> where T:EntityKey
    {
        Task<T> GetById(int id);
        Task<IReadOnlyList<T>> GetAll();
    }
}
