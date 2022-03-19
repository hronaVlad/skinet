using EFModels.Entities;

namespace EFModels.Data
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(StoreContext context):base(context)
        {
        }

        //public async Task<Product> GetProductByIdAsync(int id) => await this._context.Products
        //    .Include(_ => _.ProductType)
        //    .Include(_ => _.ProductBrand)
        //    .FirstOrDefaultAsync(_ => _.Id == id);

        //public async Task<List<Product>> GetProductsAsync() => await this._context.Products
        //        .Include(_ => _.ProductType)
        //        .Include(_ => _.ProductBrand)
        //        .ToListAsync();

    }
}