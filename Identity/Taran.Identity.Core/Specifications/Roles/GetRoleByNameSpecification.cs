using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Shared.Core.Specifications;

namespace Taran.Identity.Core.Specifications.Roles;

public class GetRoleByNameSpecification : SpecificationBase<Role>
{
    public GetRoleByNameSpecification(string name)
        : base(r => r.Name.Equals(name))
    {
    }
}