using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Commands.RoleAccesses;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Specifications.Roles;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace Taran.Identity.Application.CommandHandlers.RoleAccess;

public class UpdateRoleAccessCommandHandler : IRequestHandler<UpdateRoleAccessCommand, bool>
{
    private readonly IGenericWriteRepository<Role, int> roleWriteRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateRoleAccessCommandHandler(IGenericWriteRepository<Role, int> roleWriteRepository, IUnitOfWork unitOfWork)
    {
        this.roleWriteRepository = roleWriteRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateRoleAccessCommand request, CancellationToken cancellationToken)
    {
        var role = await roleWriteRepository.FindWithSpecification(new LoadRoleSpecification(request.RoleId)).FirstOrDefaultAsync(cancellationToken)
            ?? throw new DomainEntityNotFoundException(nameof(Role));

        role.SetRoleAccesses(request.GetUserId(), request.AccessIds.ToList());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
