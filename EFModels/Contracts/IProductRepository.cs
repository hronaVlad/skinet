using EFModels.Entities;

namespace EFModels.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetProductsAsync();
    }
}