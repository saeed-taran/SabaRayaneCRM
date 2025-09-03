using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Shared.Core.Specifications;

namespace Taran.Identity.Core.Specifications.Roles;

public class SearchRolesSpecification : SpecificationBase<Role>
{
    public SearchRolesSpecification(int skip, int take, string? term) 
        : base(r => term == null || r.Name.Contains(term))
    {
        SetPagination(skip, take);
    }
}