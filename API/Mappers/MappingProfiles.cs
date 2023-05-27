using API.Infrastucture.Extensions;
using API.Mappers.Resolvers;
using AutoMapper;
using EFModels.Entities.Identity;
using EFModels.Entities.Order;
using EFModels.Entities.Product;
using Models.Account;
using Models.Basket;
using Models.Basket.Dto;
using Models.Order;
using Models.Product;

namespace API.Mappers
{
    public class MappingProfiles : Profile
    {   
        public MappingProfiles(IConfiguration configuration)
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(_ => _.ProductType, opt => opt.MapFrom(_ => _.ProductType.Name))
            .ForMember(_ => _.ProductBrand, opt => opt.MapFrom(_ => _.ProductBrand.Name))
            .ForMember(_ => _.PictureUrl,opt => opt.MapFrom(_ => _.PictureUrl.GetAbsoluteUrl(configuration)));

            CreateMap<Product, ProductItemOrdered>()
            .ForMember(_ => _.ProductId, opt => opt.MapFrom(_ => _.Id))
            .ForMember(_ => _.ProductName, opt => opt.MapFrom(_ => _.Name))
            .ForMember(_ => _.PictureUrl, opt => opt.MapFrom(_ => _.PictureUrl.GetAbsoluteUrl(configuration)));

            CreateMap<AppUser, UserDto>()
                .ForMember(_ => _.UserName, opt => opt.MapFrom(_ => _.DisplayName));

            CreateMap<EFModels.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto, EFModels.Entities.Order.Address>();

            // basket
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            // order
            CreateMap<Order, OrderDto>()
                .ForMember(_ => _.DeliveryMethod, _ => _.MapFrom(_ => _.DeliveryMethod.ShortName))
                .ForMember(_ => _.ShippingPrice, _ => _.MapFrom(_ => _.DeliveryMethod.Price))
                .ForMember(_ => _.Status, _ => _.MapFrom(_ => _.Status));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(_ => _.Price, _ => _.MapFrom(_ => _.Price))
                .ForMember(_ => _.Quantity, _ => _.MapFrom(_ => _.Quantity))
                .ForMember(_ => _.ProductId, _ => _.MapFrom(_ => _.ItemOrdered.ProductId))
                .ForMember(_ => _.ProductName, _ => _.MapFrom(_ => _.ItemOrdered.ProductName))
                .ForMember(_ => _.PictureUrl, _ => _.MapFrom(_ => _.ItemOrdered.PictureUrl));

            CreateMap<EFModels.Entities.Order.Address, AddressDto>();
        }
    }
}