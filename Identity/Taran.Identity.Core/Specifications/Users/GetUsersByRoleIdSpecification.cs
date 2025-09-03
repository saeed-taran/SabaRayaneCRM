using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Shared.Core.Specifications;

namespace Taran.Identity.Core.Specifications.Users;

public class GetUsersByRoleIdSpecification : SpecificationBase<User>
{
    public GetUsersByRoleIdSpecification(int roleId) : base(u => u.UserRoles.Any(r => r.RoleId == roleId))
    {
    }
}
