using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Commands.Roles;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Identity.Core.Specifications.Users;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos.Languages;

namespace Taran.Identity.Application.CommandHandlers.Roles;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IGenericWriteRepository<Role, int> roleWriteRepository;
    private readonly IGenericReadRepository<Role, int> roleReadRepository;
    private readonly IGenericReadRepository<User, int> userReadRepositopry;
    private readonly IUnitOfWork unitOfWork;

    public DeleteRoleCommandHandler(IGenericWriteRepository<Role, int> roleWriteRepository, IGenericReadRepository<Role, int> roleReadRepository, IUnitOfWork unitOfWork, IGenericReadRepository<User, int> userReadRepositopry)
    {
        this.roleWriteRepository = roleWriteRepository;
        this.roleReadRepository = roleReadRepository;
        this.unitOfWork = unitOfWork;
        this.userReadRepositopry = userReadRepositopry;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var usersByThisRoleSpecification = new GetUsersByRoleIdSpecification(request.Id);
        var usersCountByThisRole = await userReadRepositopry.FindWithSpecification(usersByThisRoleSpecification).CountAsync(cancellationToken);
        if (usersCountByThisRole > 0)
            throw new DomainEntityIsInUseException();

        var role = await roleWriteRepository.GetByIdAsync(request.Id);
        if (role is null)
            throw new DomainEntityNotFoundException(nameof(KeyWords.Role));

        roleWriteRepository.Delete(role);

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}