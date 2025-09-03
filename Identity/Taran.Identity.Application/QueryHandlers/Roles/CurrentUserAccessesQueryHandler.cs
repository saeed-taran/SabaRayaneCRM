using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Queries.Roles;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Identity.Core.Specifications.Roles;
using Taran.Identity.Core.Specifications.Users;
using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos.Languages;

namespace Taran.Identity.Application.QueryHandlers.Roles;

public class CurrentUserAccessesQueryHandler : IRequestHandler<CurrentUserAccessesQuery, CurrentUserAccessesResponseDto>
{
    private readonly IGenericReadRepository<Role, int> roleReadRepository;
    private readonly IGenericReadRepository<User, int> userReadRepository;

    public CurrentUserAccessesQueryHandler(IGenericReadRepository<Role, int> roleReadRepository, IGenericReadRepository<User, int> userReadRepository)
    {
        this.roleReadRepository = roleReadRepository;
        this.userReadRepository = userReadRepository;
    }

    public async Task<CurrentUserAccessesResponseDto> Handle(CurrentUserAccessesQuery request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.FindWithSpecification(new LoadUserSpecification(request.GetUserId())).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
            throw new DomainEntityNotFoundException(nameof(KeyWords.User));

        var userRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();

        var roleAccesses = await roleReadRepository.FindWithSpecification(new GetAllRolesAccessesSpecification(userRoleIds)).FirstOrDefaultAsync(cancellationToken);
        if (roleAccesses is null)
            throw new DomainEntityNotFoundException(nameof(KeyWords.RoleAccess));

        HashSet<int> userAccesses = roleAccesses.RoleAccesses.Select(ra => ra.AccessId).Distinct().ToHashSet();

        return new(userAccesses);
    }
}
