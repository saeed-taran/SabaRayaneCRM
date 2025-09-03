using Microsoft.EntityFrameworkCore;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Shared.Core.Specifications;

namespace Taran.Identity.Core.Specifications.Roles;

public class GetAllRolesAccessesSpecification : SpecificationBase<Role>
{
    public GetAllRolesAccessesSpecification(List<int>? roleIds = null) 
        : base(r => (roleIds == null || roleIds.Contains(r.Id)))
    {
        AddInclude(r => r.Include(r => r.RoleAccesses));
    }
}
