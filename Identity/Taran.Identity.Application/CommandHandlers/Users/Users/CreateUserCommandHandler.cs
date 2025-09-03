using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Commands.Users.Users;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Identity.Core.Specifications.Users;
using Taran.Shared.Application.Services.Security;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos.Languages;

namespace Taran.Identity.Application.CommandHandlers.Users.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IGenericWriteRepository<User, int> userWriteRepository;
    private readonly IGenericReadRepository<User, int> userReadRepository;
    private readonly IGenericReadRepository<Role, int> roleReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateUserCommandHandler(IGenericWriteRepository<User, int> userWriteRepository, IGenericReadRepository<User, int> userReadRepository, IUnitOfWork unitOfWork, IGenericReadRepository<Role, int> roleReadRepository)
    {
        this.userWriteRepository = userWriteRepository;
        this.userReadRepository = userReadRepository;
        this.unitOfWork = unitOfWork;
        this.roleReadRepository = roleReadRepository;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userReadRepository.FindWithSpecification(new GetUserByUsernameSpecification(request.UserName)).FirstOrDefaultAsync(cancellationToken);
        if (existingUser is not null)
            throw new DomainEntityAlreadyExistsException();

        var role = await roleReadRepository.GetByIdAsync(request.RoleId);
        if (role is null)
            throw new DomainEntityNotFoundException(nameof(KeyWords.Role));

        var hashedPassword = PasswordHasher.Hash(request.Password);

        User user = new(request.GetUserId(), request.UserName, hashedPassword, request.FirstName, request.LastName, request.Email, request.PhoneNumber,
            request.IsActive, request.IsActiveDirectoryUser);

        user.AddRole(request.GetUserId(), request.RoleId);

        await userWriteRepository.CreateAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
