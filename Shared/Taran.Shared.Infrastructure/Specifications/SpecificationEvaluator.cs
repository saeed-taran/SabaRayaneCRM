using Taran.Shared.Core.Specifications;
using Taran.Shared.Core.Entity;

namespace Taran.Shared.Infrastructure.Specifications;

public class SpecificationEvaluator<TEntity, PrimaryKey> where TEntity : BaseEntity<PrimaryKey>
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;
        
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);
        
        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);
        
        if (spec.OrderByDescending != null)
            query = query.OrderByDescending(spec.OrderByDescending);

        query = spec.Includes.Aggregate(query, (current, include) => include(current));

        if (!spec.IgnorePagination && spec.Pagination != null)
            query = query.Skip(spec.Pagination.Skip).Take(spec.Pagination.Take);

        return query;
    }
}
