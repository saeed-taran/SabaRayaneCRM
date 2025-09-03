using Microsoft.EntityFrameworkCore;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Shared.Core.Specifications;

namespace Taran.Identity.Core.Specifications.Users;

public class SearchUserSpecification : SpecificationBase<User>
{
    public SearchUserSpecification(
        int skip, 
        int take,
        string? userName,
        string? firstName,
        string? lastName,
        string? email,
        string? phoneNumber,
        bool? isActive,
        bool? isActiveDirectoryUser
        )
        : base(u =>
            (userName == null || u.UserName == userName) &&
            (firstName == null || u.FirstName == firstName) &&
            (lastName == null || u.LastName == lastName) &&
            (email == null || u.Email == email) &&
            (phoneNumber == null || u.PhoneNumber == phoneNumber) &&
            (isActive == null || u.IsActive == isActive) &&
            (isActiveDirectoryUser == null || u.IsActiveDirectoryUser == isActiveDirectoryUser)
        )
    {
        SetPagination(skip, take);
        AddInclude(u => u.Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
        );
    }
}
