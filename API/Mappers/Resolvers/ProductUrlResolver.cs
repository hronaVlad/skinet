using API.Infrastucture.Extensions;
using AutoMapper;
using EFModels.Entities.Product;
using Models.Product;

namespace API.Mappers.Resolvers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            return source.PictureUrl.GetAbsoluteUrl(_configuration);
        }
    }
}