using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Queries.Auth;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Identity.Core.Specifications.Users;
using Taran.Identity.Dtos.Auth;
using Taran.Shared.Application.Services.Security;
using Taran.Shared.Core.Repository;

namespace Taran.Identity.Application.QueryHandlers.Auth;

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginUserResponseDto>
{
    private readonly IGenericReadRepository<User, int> userReadRepository;

    public LoginQueryHandler(IGenericReadRepository<User, int> userReadRepository)
    {
        this.userReadRepository = userReadRepository;
    }

    public async Task<LoginUserResponseDto?> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.FindWithSpecification(new GetUserByUsernameSpecification(request.UserName)).FirstOrDefaultAsync();
        if (user is null)
            return null;

        if (!PasswordHasher.Verify(request.Password, user.Password))
            return null;

        return new LoginUserResponseDto(user.Id, user.UserName, user.FirstName, user.LastName,
            user.UserRoles.Select(r => r.RoleId).ToList()
            );
    }
}
