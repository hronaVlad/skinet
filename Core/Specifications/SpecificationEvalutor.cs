using EFModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications
{
    public class SpecificationEvalutor<TEntity> where TEntity: EntityKey
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec) {

            var query = inputQuery;

            // filt4er
            if (spec.Criteria != null) {
                 query = query.Where(spec.Criteria);
            }

            // order
            if (spec.OrderBy != null) {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDesc != null) {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            // paginating
            if (spec.IsPaginationEnabled) {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // includes
            query = spec.Includes.Aggregate( query, (current, include) => current.Include(include));

            return query;
        }
    }
}