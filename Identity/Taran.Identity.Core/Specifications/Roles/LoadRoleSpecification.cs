using Microsoft.EntityFrameworkCore;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Shared.Core.Specifications;
using System.Linq.Expressions;

namespace Taran.Identity.Core.Specifications.Roles;

public class LoadRoleSpecification : SpecificationBase<Role>
{
    public LoadRoleSpecification(int id) : base(r => r.Id == id)
    {
        AddInclude(r => r.Include(r => r.RoleAccesses));
    }
}
