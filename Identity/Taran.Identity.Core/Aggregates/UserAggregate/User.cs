using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Entity;

namespace Taran.Identity.Core.Aggregates.UserAggregate;

[Index(nameof(UserName), IsUnique = true)]
public class User : AggregateRoot<int>
{
    [StringLength(32)]
    public string UserName { get; private set; }
    
    [StringLength(256)]
    public string Password { get; private set; }
    
    [StringLength(32)]
    public string? FirstName { get; private set; }
    
    [StringLength(32)]
    public string? LastName { get; private set; }
    
    [StringLength(32)]
    public string? Email { get; private set; }
    
    [StringLength(16)]
    public string? PhoneNumber { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsActiveDirectoryUser { get; private set; }

    public DateTime? LastUseDate { get; private set; }

    private List<UserRole> _UserRoles = new List<UserRole>();
    public IReadOnlyCollection<UserRole> UserRoles => _UserRoles;


    public User(int creatorUserId, string userName, string password, string? firstName, string? lastName, 
        string? email, string? phoneNumber, bool isActive, bool isActiveDirectoryUser)
        : base(creatorUserId)
    {
        UserName = DomainArgumentNullOrEmptyException.Ensure(userName, nameof(UserName));
        Password = DomainArgumentNullOrEmptyException.Ensure(password, nameof(Password));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;

        IsActive = isActive;
        IsActiveDirectoryUser = isActiveDirectoryUser;
    }

    public void Update(int userId, string userName, string? firstName, string? lastName,
        string? email, string? phoneNumber, bool isActive, bool isActiveDirectoryUser)
    {
        UserName = DomainArgumentNullOrEmptyException.Ensure(userName, nameof(UserName));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;

        IsActive = isActive;
        IsActiveDirectoryUser = isActiveDirectoryUser;

        Update(userId);
    }

    public void AddRole(int currentUserId, int roleId)
    {
        _UserRoles.Clear();

        UserRole userRole = new UserRole(currentUserId, this.Id, roleId);
        _UserRoles.Add(userRole);
    }
}