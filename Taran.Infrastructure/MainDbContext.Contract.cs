using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using SabaRayane.Contract.Core.ValueObjects;

namespace Taran.Infrastructure;

partial class MainDbContext
{
    private void ContractOnModelCreating(ModelBuilder modelBuilder, string schema)
    {
        RegisterSchemaForAssembly(modelBuilder, typeof(Customer), schema);

        CustomizeContractEntityRelations(modelBuilder);
    }

    private void CustomizeContractEntityRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().Property(c => c.MobileNumber)
            .HasConversion(c => c.Value, c => MobileNumber.Create(c));
    }

    internal DbSet<Customer> Customer { get; private set; }
    internal DbSet<Product> Product { get; private set; }
}
