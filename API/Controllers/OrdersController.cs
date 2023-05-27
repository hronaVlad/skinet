using API.Infrastucture.Errors;
using API.Infrastucture.Extensions;
using AutoMapper;
using Core.Repositories.Contracts;
using Core.Services.Contracts;
using EFModels.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Order;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IBasketRepository basketRepository, IMapper mapper)
        {
            _orderService = orderService;
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetForUser()
        {
            var email = User.GetEmail();

            IReadOnlyList<Order> results = await _orderService.GetAllAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(results));
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDto>> Get(int orderId)
        {
            var email = User.GetEmail();

            Order result = await _orderService.GetByIdAsync(orderId, email);

            if (result == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<OrderDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] OrderRequestDto requestDto)
        {
            var email = User.GetEmail();
            var basket = await _basketRepository.GetAsync(requestDto.basketId);

            if (basket?.Items?.Any() == null) 
                return BadRequest(new ApiResponse(400, "No items in the basket"));

            var order = await _orderService.CreateOrderAsync(email, basket, requestDto.DeliveryMethodId, requestDto.ShipToAddress);

            if (order == null) 
                return BadRequest(new ApiResponse(400, "Failed to create an order"));

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodAsync());
        }
    }
}
