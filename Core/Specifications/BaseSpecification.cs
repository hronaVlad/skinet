using EFModels.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : EntityKey
    {
        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }
            
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = 
            new List<Expression<Func<T, object>>>();

        public void AddInclude(Expression<Func<T, object>> include)
        {
            this.Includes.Add(include);
        }

        public Expression<Func<T, object>> OrderBy  {get; private set;}
        public Expression<Func<T, object>> OrderByDesc {get; private set;}
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)  => this.OrderBy = orderByExpression;
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)  => this.OrderByDesc = orderByDescExpression;

        
        protected void ApplyPaging(int pageIndex, int pageSize) {
            this.Take = pageSize;
            this.Skip = (pageIndex - 1) * pageSize;
        }

        public int Take { get; private set; }
        public int Skip  { get; private set; }
    }
}
