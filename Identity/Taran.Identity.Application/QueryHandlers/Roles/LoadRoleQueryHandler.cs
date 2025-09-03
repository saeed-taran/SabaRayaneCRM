using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Queries.Roles;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Specifications.Roles;
using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace Taran.Identity.Application.QueryHandlers.Roles;

public class LoadRoleQueryHandler : IRequestHandler<LoadRoleQuery, LoadRoleResponseDto>
{
    private IGenericReadRepository<Role, int> roleReadRepository;

    public LoadRoleQueryHandler(IGenericReadRepository<Role, int> roleReadRepository)
    {
        this.roleReadRepository = roleReadRepository;
    }

    public async Task<LoadRoleResponseDto> Handle(LoadRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await roleReadRepository.FindWithSpecification(new LoadRoleSpecification(request.Id)).FirstOrDefaultAsync(cancellationToken);
        if (role is null)
            throw new DomainEntityNotFoundException(nameof(Role));

        return new LoadRoleResponseDto(role.Id, role.Name, role.Title, role.RoleAccesses.Select(ra => ra.AccessId).ToList());
    }
}