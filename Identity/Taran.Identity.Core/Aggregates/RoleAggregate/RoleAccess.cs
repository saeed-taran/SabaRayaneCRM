using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Entity;

namespace Taran.Identity.Core.Aggregates.RoleAggregate;

[Index(nameof(RoleId), nameof(AccessId), IsUnique = true)]
public class RoleAccess : BaseEntity<int>
{
    public int RoleId { get; private set; }
    public Role Role { get; private set; }

    public int AccessId { get; private set; }

    internal RoleAccess(int creatorUserId, int accessId) : base(creatorUserId)
    {
        AccessId = accessId;
    }
}