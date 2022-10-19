using EFModels.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T> where T : EntityKey
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>>  Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesc { get;}

        int Take {get;}
        int Skip {get;}
        bool IsPaginationEnabled => Take > 0;
    }
}
