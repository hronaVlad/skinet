using EFModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications
{
    public class SpecificationEvalutor<TEntity> where TEntity: EntityKey
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec) {

            var query = inputQuery;

            if (spec.Criteria != null) {
                 query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate( query, (current, include) => current.Include(include));

            return query;
        }
    }
}