using EFModels.Entities.Order;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.Property(_ => _.SubTotal).HasColumnType("decimal(18,2)");

            builder.OwnsOne(_ => _.ShipToAddress, _ => _.WithOwner());

            builder.Property(_ => _.Status).HasConversion(
                _ => _.ToString(),
                _ => (OrderStatus)Enum.Parse(typeof(OrderStatus), _));

            builder.HasMany(_ => _.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
