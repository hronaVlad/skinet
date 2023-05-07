using System.Text.Json;
using Microsoft.Extensions.Logging;
using EFModels.Entities.Product;
using EFModels.Entities.Order;

namespace EFModels.Seed
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {

            var logger = loggerFactory.CreateLogger<StoreContextSeed>();

            try
            {
                if (!context.Products.Any())
                {

                    await SeedBrandsAsync(context);
                    await SeedTypesAsync(context);
                    await SeedProductsAsync(context);
                    await SeedDeliveryMethodsAsync(context);

                    await context.SaveChangesAsync();
                }

                if (!context.DeliveryMethods.Any())
                {
                    await SeedDeliveryMethodsAsync(context);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occured during data seed");
                throw;
            }
        }

        private static async Task SeedBrandsAsync(StoreContext context)
        {
            var brandData = File.ReadAllText(@"..\EFModels\Seed\SeedData\brands.json");

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

            if (brands != null)
            {
                foreach (var brand in brands)
                {
                    await context.ProductBrands.AddAsync(brand);
                }
            }
        }

        private static async Task SeedTypesAsync(StoreContext context)
        {
            var typeData = File.ReadAllText(@"..\EFModels\Seed\SeedData\types.json");

            var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

            if (types != null)
            {
                foreach (var type in types)
                {
                    await context.ProductTypes.AddAsync(type);
                }
            }
        }

        private static async Task SeedProductsAsync(StoreContext context)
        {
            var productData = File.ReadAllText(@"..\EFModels\Seed\SeedData\products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productData);

            if (products != null)
            {
                foreach (var product in products)
                {
                    await context.Products.AddAsync(product);
                }
            }
        }

        private static async Task SeedDeliveryMethodsAsync(StoreContext context)
        {
            var data = File.ReadAllText(@"..\EFModels\Seed\SeedData\delivery.json");

            var list = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);

            if (list != null)
            {
                foreach (var item in list)
                {
                    await context.DeliveryMethods.AddAsync(item);
                }
            }
        }
    }
}