using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Queries.Roles;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Specifications.Roles;
using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Core.Repository;

namespace Taran.Identity.Application.QueryHandlers.Roles;

public class GetAllRolesAccessesQueryHandler : IRequestHandler<GetAllRolesAccessesQuery, GetAllRolesAccessesResponseDto>
{
    private IGenericReadRepository<Role, int> roleReadRepository;

    public GetAllRolesAccessesQueryHandler(IGenericReadRepository<Role, int> roleReadRepository)
    {
        this.roleReadRepository = roleReadRepository;
    }

    public async Task<GetAllRolesAccessesResponseDto> Handle(GetAllRolesAccessesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleReadRepository.FindWithSpecification(new GetAllRolesAccessesSpecification()).ToListAsync(cancellationToken);

        Dictionary<int, HashSet<int>> roleAccessDictionary = roles.ToDictionary(r => r.Id, r => r.RoleAccesses.Select(ra => ra.AccessId).ToHashSet());

        return new GetAllRolesAccessesResponseDto(roleAccessDictionary);
    }
}