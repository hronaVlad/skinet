using API.Helpers;
using AutoMapper;
using EFModels.Entities;
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
        }
    }
}