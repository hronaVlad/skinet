using Models.Basket;

namespace Core.Repositories.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetAsync(string id);
        Task<CustomerBasket> UpdateAsync(CustomerBasket basket);
        Task<bool> DeleteAsync(string id);
    }
}
