using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Specifications;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Repository;
using Taran.Shared.Infrastructure.Specifications;

namespace Taran.Shared.Infrastructure.Repositories;

public class GenericReadRepository<TEntity, PrimaryKey> : IGenericReadRepository<TEntity, PrimaryKey> where TEntity : BaseEntity<PrimaryKey>
{
    private readonly DbContext dbContext;

    public GenericReadRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(PrimaryKey id)
    {
        return await dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public IQueryable<TEntity> FindWithSpecification(ISpecification<TEntity> specification)
    {
        return SpecificationEvaluator<TEntity, PrimaryKey>
            .GetQuery(dbContext
                .Set<TEntity>()
                .AsNoTracking()
                .AsQueryable(), specification
            );
    }
}