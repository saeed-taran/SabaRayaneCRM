using Microsoft.EntityFrameworkCore;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Aggregates.UserAggregate;

namespace Taran.Infrastructure;

partial class MainDbContext
{
    private void IdentityOnModelCreating(ModelBuilder modelBuilder, string schema)
    {
        RegisterSchemaForAssembly(modelBuilder, typeof(Role), schema);

        modelBuilder.Entity<User>()
        .Property(a => a.Password).HasColumnType("nvarchar(100)");

        CustomizeIdentityEntityRelations(modelBuilder);
    }

    private void CustomizeIdentityEntityRelations(ModelBuilder modelBuilder)
    {
        
    }

    internal DbSet<User> User { get; private set; }
    internal DbSet<Role> Role { get; private set; }
}
