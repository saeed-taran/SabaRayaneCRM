using Microsoft.EntityFrameworkCore;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Shared.Core.Specifications;

namespace Taran.Identity.Core.Specifications.Users;

public class LoadUserSpecification : SpecificationBase<User>
{
    public LoadUserSpecification(int userId) : base(u => u.Id == userId)
    {
        AddInclude(u => u.Include(u => u.UserRoles));
    }
}
