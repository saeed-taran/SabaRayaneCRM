using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.User;

namespace Taran.Infrastructure;

public partial class MainDbContext : DbContext
{
    private readonly IAppUser appUser;

    public MainDbContext(DbContextOptions options, IAppUser appUser) : base(options)
    {
        this.appUser = appUser;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        IdentityOnModelCreating(modelBuilder, "identity");
        ContractOnModelCreating(modelBuilder, "contract");
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime");
        configurationBuilder.Properties<string>().HaveColumnType("varchar");
    }
    
    private void RegisterSchemaForAssembly(ModelBuilder modelBuilder, Type sampleEntityInTargetAssembly, string schema) 
    {
        List<string> includeEntityWithBaseTypeNames = new() 
        {
            typeof(AggregateRoot<>).Name,
            typeof(BaseEntity<>).Name
        };

        List<string> excludeTypes = new()
        {
            
        };

        var entityTypesContainingBaseType = sampleEntityInTargetAssembly.Assembly.GetTypes()
                .Where(t => t.BaseType != null)
                .Where(t => includeEntityWithBaseTypeNames.Contains(t.BaseType!.Name))
                .Where(t => !excludeTypes.Contains(t.Name))
                ;

        if (entityTypesContainingBaseType.Count() == 0)
            throw new Exception("No entity found in provided assembly, for schema:" + schema);

        foreach (var entityType in entityTypesContainingBaseType)
        {
            modelBuilder.Entity(entityType).ToTable(entityType.Name, schema);
        }
    }

    private void RegisterSchemaForListOfEntityTypes(ModelBuilder modelBuilder, List<Type> entityTypes, string schema)
    {
        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType).ToTable(entityType.Name, schema);
        }
    }
}