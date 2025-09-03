using Taran.Shared.Core.Specifications;
using Taran.Shared.Core.Entity;

namespace Taran.Shared.Core.Repository;

public interface IGenericReadRepository<TEntity, PrimaryKey> where TEntity : BaseEntity<PrimaryKey>
{
    Task<TEntity?> GetByIdAsync(PrimaryKey id);
    IQueryable<TEntity> FindWithSpecification(ISpecification<TEntity> specification);
}