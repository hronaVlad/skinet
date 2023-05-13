using Core.Specifications;
using EFModels.Entities;

namespace Core.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : EntityKey
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
    }
}
