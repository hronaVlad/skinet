using Core.Repositories.Contracts;
using EFModels;
using EFModels.Entities;
using System.Collections;

namespace Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly Hashtable _cache = new Hashtable();

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> Complete() =>
            await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> GetRepository<T>() where T : EntityKey
        {
            var name = typeof(T).Name;
            object? instance;

            if (_cache.ContainsKey(name))
            {
                instance = _cache[name];
            }
            else
            {
                instance = Activator.CreateInstance(
                    typeof(GenericRepository<>).MakeGenericType(typeof(T)),
                    _context);

                _cache.Add(name, instance);
            }

            return instance as IGenericRepository<T>;
        }
    }
}
