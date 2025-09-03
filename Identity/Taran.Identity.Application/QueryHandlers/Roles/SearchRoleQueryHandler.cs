using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Queries.Roles;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Specifications.Roles;
using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos;

namespace Taran.Identity.Application.QueryHandlers.Roles;

public class SearchRoleQueryHandler : IRequestHandler<SearchRoleQuery, PaginatedResponseDto<SearchRoleResponseDto>>
{
    private readonly IGenericReadRepository<Role, int> roleRepository;

    public SearchRoleQueryHandler(IGenericReadRepository<Role, int> roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public async Task<PaginatedResponseDto<SearchRoleResponseDto>> Handle(SearchRoleQuery request, CancellationToken cancellationToken)
    {
        var specification = new SearchRolesSpecification(request.Skip, request.Take, request.Term);

        var roles = await roleRepository.FindWithSpecification(specification).ToListAsync(cancellationToken);

        specification.SetIgnorePagination(true);
        var totalCount = await roleRepository.FindWithSpecification(specification).CountAsync(cancellationToken);

        var roleListDtos = roles.Select(r => new SearchRoleResponseDto(
            r.Id, r.Name, r.Title
        )).ToList();

        return new(request.Skip, request.Take, totalCount, roleListDtos);
    }
}