using EFModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products {get;set;}
    }
}