using EFModels.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(_ => _.Id).IsRequired();
            builder.Property(_ => _.Name).IsRequired().HasMaxLength(100);
            builder.Property(_ => _.Description).IsRequired().HasMaxLength(180);
            builder.Property(_ => _.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(_ => _.ProductType).WithMany()
                .HasForeignKey(_ => _.ProductTypeId);
            builder.HasOne(_ => _.ProductBrand).WithMany()
                .HasForeignKey(_ => _.ProductBrandId);

        }
    }
}