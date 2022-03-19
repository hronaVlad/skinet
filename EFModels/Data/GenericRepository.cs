using EFModels.Contracts;
using EFModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityKey
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAll() =>
            await _context.Set<T>().ToListAsync();
        

        public async Task<T> GetById(int id) =>
            await _context.Set<T>().FindAsync(id);
        
    }
}
