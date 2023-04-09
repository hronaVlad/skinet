using API.Helpers;
using AutoMapper;
using EFModels.Entities;
using EFModels.Entities.Identity;
using Models.Account;
using Models.Basket;
using Models.Basket.Dto;
using Models.Product;

namespace API.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(_ => _.ProductType, opt => opt.MapFrom(_ => _.ProductType.Name))
            .ForMember(_ => _.ProductBrand, opt => opt.MapFrom(_ => _.ProductBrand.Name))
            .ForMember(_ => _.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<AppUser, UserDto>();
            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasket, CustomerBasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}