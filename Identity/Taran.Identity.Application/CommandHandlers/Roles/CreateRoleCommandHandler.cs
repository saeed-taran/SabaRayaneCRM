using MediatR;
using Taran.Identity.Application.Commands.Roles;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Specifications.Roles;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace Taran.Identity.Application.CommandHandlers.Roles;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
{
    private readonly IGenericWriteRepository<Role, int> roleWriteRepository;
    private readonly IGenericReadRepository<Role, int> roleReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateRoleCommandHandler(IGenericWriteRepository<Role, int> roleWriteRepository, IGenericReadRepository<Role, int> roleReadRepository, IUnitOfWork unitOfWork)
    {
        this.roleWriteRepository = roleWriteRepository;
        this.roleReadRepository = roleReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleByNameSpecification = new GetRoleByNameSpecification(request.Name);
        var existingRole = roleReadRepository.FindWithSpecification(roleByNameSpecification).FirstOrDefault();
        if (existingRole is not null)
            throw new DomainEntityAlreadyExistsException();

        Role newRole = new(request.GetUserId(), request.Name, request.Title);
        await roleWriteRepository.CreateAsync(newRole);

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}