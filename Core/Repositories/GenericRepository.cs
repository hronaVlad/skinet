using Core.Repositories.Contracts;
using Core.Specifications;
using EFModels.Data;
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

        public async Task<T> GetById(int id) =>
            await _context.Set<T>().FindAsync(id);
            
        public async Task<IReadOnlyList<T>> GetAll() =>
            await _context.Set<T>().ToListAsync();

        public async Task<T> GetWithSpec(ISpecification<T> spec) =>
            await SpecificationEvalutor<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> GetAll(ISpecification<T> spec) =>
            await SpecificationEvalutor<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).ToListAsync();
    }
}
