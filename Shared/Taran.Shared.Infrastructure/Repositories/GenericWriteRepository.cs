using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;
using Taran.Shared.Core.Specifications;
using Taran.Shared.Infrastructure.Specifications;

namespace Taran.Shared.Infrastructure.Repositories;

public class GenericWriteRepository<TAggregateRoot, PrimaryKey> : IGenericWriteRepository<TAggregateRoot, PrimaryKey> where TAggregateRoot : AggregateRoot<PrimaryKey>
{
    private readonly DbContext dbContext;

    public GenericWriteRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(TAggregateRoot entity)
    {
        await dbContext
            .Set<TAggregateRoot>()
            .AddAsync(entity);
    }

    public void Delete(TAggregateRoot aggregateRoot)
    {
        if (aggregateRoot is null)
            throw new DomainArgumentNullOrEmptyException();

        dbContext
            .Set<TAggregateRoot>()
            .Remove(aggregateRoot);
    }

    public async Task<TAggregateRoot?> GetByIdAsync(PrimaryKey id)
    {
        return await dbContext
            .Set<TAggregateRoot>()
            .FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<TAggregateRoot?> GetByIdAsyncByIgnoreQueryFilter(PrimaryKey id)
    {
        return await dbContext
            .Set<TAggregateRoot>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(a => a.Id.Equals(id));
    }
    public IQueryable<TAggregateRoot> FindWithSpecification(ISpecification<TAggregateRoot> specification)
    {
        return SpecificationEvaluator<TAggregateRoot, PrimaryKey>
            .GetQuery(dbContext
                .Set<TAggregateRoot>()
                .AsQueryable(), specification
            );
    }
}