using Microsoft.EntityFrameworkCore;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Shared.Core.Specifications;

namespace Taran.Identity.Core.Specifications.Users;

public class GetUserByUsernameSpecification : SpecificationBase<User>
{
    public GetUserByUsernameSpecification(string username) 
        : base(u => u.UserName == username)
    {
        AddInclude(q => q.Include(u => u.UserRoles));
    }
}
