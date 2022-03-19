using Core.Specifications;
using EFModels.Entities;

namespace Core.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : EntityKey
    {
        Task<T> GetById(int id);
        Task<T> GetWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> GetAll(ISpecification<T> spec);
    }
}
