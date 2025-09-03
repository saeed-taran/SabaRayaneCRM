using Microsoft.EntityFrameworkCore;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Shared.Core.Entity;

namespace Taran.Identity.Core.Aggregates.UserAggregate;

[Index(nameof(UserId), nameof(RoleId), IsUnique = true)]
public class UserRole : BaseEntity<int>
{
    public int UserId { get; private set; }
    public User User { get; private set; }

    public int RoleId { get; private set; }
    public Role Role { get; private set; }

    internal UserRole(int creatorUserId, int userId, int roleId) : base(creatorUserId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}
