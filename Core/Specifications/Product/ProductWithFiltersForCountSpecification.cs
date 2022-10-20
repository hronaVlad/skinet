using P = EFModels.Entities.Product;


namespace Core.Specifications.Product
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<P>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
                 : base(_ => (!productSpecParams.BrandId.HasValue || _.ProductBrandId == productSpecParams.BrandId.Value) &&
                             (!productSpecParams.TypeId.HasValue || _.ProductTypeId == productSpecParams.TypeId.Value) &&
                             (string.IsNullOrEmpty(productSpecParams.Search) ||  _.Name.ToLower().Contains(productSpecParams.Search.ToLower())))
        {
            
        }
    }
}
