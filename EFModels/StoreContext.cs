    using System.Reflection;
using EFModels.Entities.Order;
using EFModels.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFModels
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Order> Orders { get; set;  }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var decimalProperties = entityType.ClrType.GetProperties().Where(_ => _.PropertyType ==  typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties().Where(_ => _.PropertyType == typeof(DateTimeOffset));

                    foreach(var decimalProperty in decimalProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(decimalProperty.Name).HasConversion<double>();
                    }

                    foreach (var dateTimePropertiy in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(dateTimePropertiy.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}