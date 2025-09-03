using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Queries.Users.Users;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Identity.Core.Specifications.Users;
using Taran.Identity.Dtos.Users.Users;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace Taran.Identity.Application.QueryHandlers.Users.Users;

public class LoadUserQueryHandler : IRequestHandler<LoadUserQuery, LoadUserResponseDto>
{
    private readonly IGenericReadRepository<User, int> userReadRepository;

    public LoadUserQueryHandler(IGenericReadRepository<User, int> userReadRepository)
    {
        this.userReadRepository = userReadRepository;
    }

    public async Task<LoadUserResponseDto> Handle(LoadUserQuery request, CancellationToken cancellationToken)
    {
        var specification = new LoadUserSpecification(request.Id);
        var user = await userReadRepository.FindWithSpecification(specification).FirstOrDefaultAsync() 
            ?? throw new DomainEntityNotFoundException(nameof(User));

        LoadUserResponseDto response = new LoadUserResponseDto(user.Id, user.UserName, user.UserRoles.Select(r => r.RoleId).ToList());

        return response;
    }
}
