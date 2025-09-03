using MediatR;
using Taran.Identity.Application.Commands.Roles;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Specifications.Roles;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos.Languages;

namespace Taran.Identity.Application.CommandHandlers.Roles;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
{
    private readonly IGenericWriteRepository<Role, int> roleWriteRepository;
    private readonly IGenericReadRepository<Role, int> roleReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateRoleCommandHandler(IGenericWriteRepository<Role, int> roleWriteRepository, IGenericReadRepository<Role, int> roleReadRepository, IUnitOfWork unitOfWork)
    {
        this.roleWriteRepository = roleWriteRepository;
        this.roleReadRepository = roleReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleByNameSpecification = new GetRoleByNameSpecification(request.Name);
        var existingRole = roleReadRepository.FindWithSpecification(roleByNameSpecification).FirstOrDefault();
        if (existingRole is not null && existingRole.Id != request.Id)
            throw new DomainEntityAlreadyExistsException();

        var role = await roleWriteRepository.GetByIdAsync(request.Id);
        if (role is null)
            throw new DomainEntityNotFoundException(nameof(KeyWords.Role));

        role.Update(request.GetUserId(), request.Name, request.Title);

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}