using API.Infrastucture.Errors;
using API.Infrastucture.Extensions;
using AutoMapper;
using Core.Repositories;
using Core.Repositories.Contracts;
using Core.Services.Contracts;
using EFModels.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Order;
using Models.Payment;

namespace API.Controllers
{
    // [Authorize]
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IUnitOfWork unitOfWork, IOrderService orderService,  IMapper mapper)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateIntent(IntentRequestModel model)
        {

            if (model == null)
                return BadRequest();

            // var email = User.GetEmail();
            // var order = await _orderService.GetByIdAsync(orderId, email);

            // if (order == null) 
            //     return NotFound(new ApiResponse(404));

            // var intentId = order.PaymentIntentId;
            // var total = (long)order.GetTotal();

            StripeIntentResponse response = null;

            if (model.IntentId.IsNullOrEmpty())
            {
                response = await _paymentService.CreateIntentAsync(model.Total);

            //     order.PaymentIntentId = response.IntentId;
            //     order.PaymentClientSecret = response.ClientSecret;

            //     _unitOfWork.GetRepository<Order>().Update(order);

            //     await _unitOfWork.CompleteAsync();
            }
            else
            {
                response = await _paymentService.UpdateIntentAsync(model.IntentId, model.Total);
            }

            // return Ok(_mapper.Map<OrderDto>(order));

            return Ok(response);
        }
    }
}
