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
            string apiUrl = _configuration["ApiUrl"];
            string result = null;

            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                result = $"{apiUrl}/{source.PictureUrl}";
            }

            return result;
        }
    }
}