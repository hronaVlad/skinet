using API.Mappers.Resolvers;
using AutoMapper;
using EFModels.Entities.Identity;
using EFModels.Entities.Product;
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

            CreateMap<AppUser, UserDto>()
                .ForMember(_ => _.UserName, opt => opt.MapFrom(_ => _.DisplayName));
            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasket, CustomerBasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}