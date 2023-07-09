using EFModels.Entities.Order;
using Models.Basket;
using Models.Account;
using Core.Services.Contracts;
using Core.Repositories.Contracts;
using AutoMapper;
using EFModels.Entities.Product;
using Core.Specifications.Order;

namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrderAsync(string email, CustomerBasket basket, int deliverMethodId, AddressDto addressDto, string intnetId)
        {
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod>().GetByIdAsync(deliverMethodId);
            var address = _mapper.Map<Address>(addressDto);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(item.Id);

                items.Add(new OrderItem
                {
                    ItemOrdered = _mapper.Map<ProductItemOrdered>(product),
                    Price = (decimal)product.Price,
                    Quantity = item.Quantity
                });
            }

            var subTotal = items.Sum(_ => _.Price * _.Quantity);

            try
            {
                var order = new Order(email, address, deliveryMethod, items, intnetId, subTotal);

                _unitOfWork.GetRepository<Order>().Add(order);

                var result = await _unitOfWork.CompleteAsync();

                if (result <= 0) return null;

                await _basketRepository.DeleteAsync(basket.Id);

                return order;
            }
            catch(Exception ex) {
                throw;
            }
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(string email)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var specification = new OrderWithItemsAndOrderingSpecification(email);

            var list = await orderRepository.GetAllAsync(specification);

            return list;
        }

        public async Task<Order> GetByIdAsync(int id, string email)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();
            var specification = new OrderWithItemsAndOrderingSpecification(id, email);

            var record = await orderRepository.GetWithSpecAsync(specification);

            return record;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.GetRepository<DeliveryMethod>().GetAllAsync();
        }
    }
}
