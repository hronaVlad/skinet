using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using P = EFModels.Entities.Product;

namespace Core.Specifications.Product
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<P>
    {
        public ProductWithTypesAndBrandsSpecification()
        {
            Include();
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
    }
}
