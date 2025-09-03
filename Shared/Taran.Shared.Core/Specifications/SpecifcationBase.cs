using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Taran.Shared.Core.Specifications;

public class SpecificationBase<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public SpecificationPagination? Pagination { get; private set; }

    public bool IgnorePagination { get; private set; }

    public SpecificationBase(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public void SetIgnorePagination(bool ignorePagination)
    {
        IgnorePagination = ignorePagination;
    }

    protected void SetPagination(int skip, int take)
    {
        Pagination = new SpecificationPagination(skip, take);
    }

    protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    protected void SetOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void SetOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }
}