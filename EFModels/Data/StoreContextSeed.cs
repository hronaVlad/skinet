using System.Text.Json;
using Microsoft.Extensions.Logging;
using EFModels.Entities;

namespace EFModels.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync (StoreContext context, ILoggerFactory loggerFactory) {
            
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();

            try
            {
                if (!context.Products.Any()) {
                    
                    await SeedBrandsAsync(context);
                    await SeedTypesAsync(context);
                    await SeedProductsAsync(context);

                    await context.SaveChangesAsync();
                }
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "An error occured during data seed");
                throw;
            }
        }

        private static async Task SeedBrandsAsync(StoreContext context)
        {
            var brandData = File.ReadAllText(@"..\EFModels\Data\SeedData\brands.json");

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

            if (brands != null) {
                foreach (var brand in brands) {
                    await context.ProductBrands.AddAsync(brand);
                }
            }
        }

        private static async Task SeedTypesAsync(StoreContext context)
        {
            var typeData = File.ReadAllText(@"..\EFModels\Data\SeedData\types.json");

            var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

            if (types != null) {
                foreach (var type in types) {
                    await context.ProductTypes.AddAsync(type);
                }
            }
        }

        private static async Task SeedProductsAsync(StoreContext context)
        {
            var productData = File.ReadAllText(@"..\EFModels\Data\SeedData\products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productData);

            if (products != null) {
                foreach (var product in products) {
                    await context.Products.AddAsync(product);
                }
            }
        }
    }
}