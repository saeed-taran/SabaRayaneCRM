using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Commands.Users.Users;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Identity.Core.Specifications.Users;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos.Languages;

namespace Taran.Identity.Application.CommandHandlers.Users.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IGenericWriteRepository<User, int> userWriteRepository;
    private readonly IGenericReadRepository<User, int> userReadRepository;
    private readonly IGenericReadRepository<Role, int> roleReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateUserCommandHandler(IGenericWriteRepository<User, int> userWriteRepository, IGenericReadRepository<User, int> userReadRepository, IUnitOfWork unitOfWork, IGenericReadRepository<Role, int> roleReadRepository)
    {
        this.userWriteRepository = userWriteRepository;
        this.userReadRepository = userReadRepository;
        this.unitOfWork = unitOfWork;
        this.roleReadRepository = roleReadRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userReadRepository.FindWithSpecification(new GetUserByUsernameSpecification(request.UserName)).ToListAsync(cancellationToken);
        if (existingUser is not null && existingUser.Any(u => u.Id != request.Id))
            throw new DomainEntityAlreadyExistsException();

        var user = await userWriteRepository.FindWithSpecification(new LoadUserSpecification(request.Id)).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
            throw new DomainEntityNotFoundException(nameof(KeyWords.User));

        var role = await roleReadRepository.GetByIdAsync(request.RoleId);
        if (role is null)
            throw new DomainEntityNotFoundException(nameof(KeyWords.Role));

        user.Update(request.GetUserId(), request.UserName, request.FirstName, request.LastName, request.Email, request.PhoneNumber,
            request.IsActive, request.IsActiveDirectoryUser);

        user.AddRole(request.GetUserId(), request.RoleId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
