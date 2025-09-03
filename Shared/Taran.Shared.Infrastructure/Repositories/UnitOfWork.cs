using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Repository;

namespace Taran.Shared.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
