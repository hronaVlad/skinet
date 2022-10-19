using AutoMapper;
using EFModels.Entities;
using Models.Product;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            string apiUrl =  _configuration["ApiUrl"];
            string result = null;

            if (!string.IsNullOrEmpty(source.PictureUrl)) {
                result = $"{apiUrl}/{source.PictureUrl}";
            }

            return result;
        }
    }
}