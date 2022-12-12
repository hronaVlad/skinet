using P = EFModels.Entities.Product;

namespace Core.Specifications.Product
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<P>
    {
        public ProductWithTypesAndBrandsSpecification()
        {
        }

        public ProductWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams) 
            : base (_ => (!productSpecParams.BrandId.HasValue || _.ProductBrandId == productSpecParams.BrandId.Value) &&
                         (!productSpecParams.TypeId.HasValue || _.ProductTypeId == productSpecParams.TypeId.Value) &&
                         (string.IsNullOrEmpty(productSpecParams.Search) || _.Name.ToLower().Contains(productSpecParams.Search.ToLower())))
        {
            Include();
            Sort(productSpecParams);
            ApplyPaging(productSpecParams.PageIndex, productSpecParams.PageSize);
        }

        public ProductWithTypesAndBrandsSpecification(int id) 
            : base(_ => _.Id == id)
        {
            Include();
        }

        private void Include(){
            AddInclude(_ => _.ProductType);
            AddInclude(_ => _.ProductBrand);
        }

        private void Sort(ProductSpecParams productSpecParams) {
            if (string.IsNullOrEmpty(productSpecParams.Sort))
                productSpecParams.Sort = "name";

            switch (productSpecParams.Sort) {
                case "priceAsc":
                    AddOrderBy(_ => _.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(_ => _.Price);
                    break;
                case "name":
                    AddOrderBy(_ => _.Name);
                    break;
                default:
                    AddOrderBy(_ => _.Id);
                    break;
            }
        }  
    }
}
