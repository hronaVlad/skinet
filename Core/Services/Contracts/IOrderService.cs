using EFModels.Entities.Order;
using Models.Account;
using Models.Basket;
using System.Collections.Generic;

namespace Core.Services.Contracts
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string email, CustomerBasket basket, int deliverMethodId, AddressDto address);
        Task<IReadOnlyList<Order>> GetAllAsync(string email);
        Task<Order> GetByIdAsync(int id, string email);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
