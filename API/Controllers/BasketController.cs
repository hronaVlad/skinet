using AutoMapper;
using Core.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Models.Basket;
using Models.Basket.Dto;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody] CustomerBasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);

            var updatedBasket = await _basketRepository.UpdateAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteAsync(id);
        }
    }
}
