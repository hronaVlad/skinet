using Core.Repositories.Contracts;
using Core.Specifications;
using EFModels;
using EFModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityKey
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);
            
        public async Task<T> GetWithSpecAsync(ISpecification<T> spec) =>
            await ApplySpecification(spec).FirstOrDefaultAsync();


        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec) =>
            await ApplySpecification(spec).ToListAsync();


        public async Task<int> CountAsync(ISpecification<T> spec) =>
            await ApplySpecification(spec).CountAsync();


        private IQueryable<T> ApplySpecification(ISpecification<T> spec) =>
            SpecificationEvalutor<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
