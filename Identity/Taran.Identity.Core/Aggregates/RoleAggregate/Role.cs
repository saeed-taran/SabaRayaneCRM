using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Entity;
using System.ComponentModel.DataAnnotations;
using Taran.Identity.Core.Aggregates.UserAggregate;

namespace Taran.Identity.Core.Aggregates.RoleAggregate;

[Index(nameof(Name), IsUnique = true)]
public class Role : AggregateRoot<int>
{
    [StringLength(32)]
    public string Name { get; private set; }
    
    [StringLength(32)]
    public string? Title { get; private set; }

    private List<RoleAccess> _roleAccesses = new();
    public IReadOnlyCollection<RoleAccess> RoleAccesses => _roleAccesses;

    private List<UserRole> _RoleUsers = new List<UserRole>();
    public IReadOnlyCollection<UserRole> RoleUsers => _RoleUsers;

    public Role(int creatorUserId, string name, string? title) : base(creatorUserId)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Title = title;
    }

    public void Update(int userId, string name, string? title)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Title = title;

        Update(userId);
    }

    public void SetRoleAccesses(int creatorUserId, List<int> roleAccesses)
    {
        _roleAccesses.Clear();
        _roleAccesses.AddRange(roleAccesses.Select(a => new RoleAccess(creatorUserId, a)).ToList());
    }
}