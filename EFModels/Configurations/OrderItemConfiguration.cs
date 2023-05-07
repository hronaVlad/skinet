using EFModels.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFModels.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(_ => _.ItemOrdered, _ =>
            {
                _.WithOwner();
            });

            builder.Property(_ => _.Price).HasColumnType("decimal(18,2)");
        }
    }
}
