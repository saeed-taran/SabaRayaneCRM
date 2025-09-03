using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Specifications;

namespace Taran.Shared.Core.Repository;

public interface IGenericWriteRepository<AggregateRoot, PrimaryKey> where AggregateRoot : AggregateRoot<PrimaryKey>
{
    Task<AggregateRoot?> GetByIdAsync(PrimaryKey id);
    Task<AggregateRoot?> GetByIdAsyncByIgnoreQueryFilter(PrimaryKey id);
    IQueryable<AggregateRoot> FindWithSpecification(ISpecification<AggregateRoot> specification);
    Task CreateAsync(AggregateRoot aggregateRoot);
    void Delete(AggregateRoot aggregateRoot);
}
