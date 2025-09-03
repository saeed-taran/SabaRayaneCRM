using MediatR;
using Microsoft.EntityFrameworkCore;
using Taran.Identity.Application.Queries.Users.Users;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Identity.Core.Specifications.Users;
using Taran.Identity.Dtos.Users.Users;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos;

namespace Taran.Identity.Application.QueryHandlers.Users.Users;

public class SearchUserQueryHandler : IRequestHandler<SearchUserQuery, PaginatedResponseDto<SearchUserResponseDto>>
{
    private readonly IGenericReadRepository<User, int> userReadRepository;

    public SearchUserQueryHandler(IGenericReadRepository<User, int> userReadRepository)
    {
        this.userReadRepository = userReadRepository;
    }

    public async Task<PaginatedResponseDto<SearchUserResponseDto>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
    {
        var specification = new SearchUserSpecification(request.Skip, request.Take, request.UserName, request.FirstName, request.LastName,
            request.Email, request.PhoneNumber, request.IsActive, request.IsActiveDirectoryUser);

        var users = await userReadRepository.FindWithSpecification(specification).ToListAsync(cancellationToken);

        specification.SetIgnorePagination(true);
        var totalCount = await userReadRepository.FindWithSpecification(specification).CountAsync(cancellationToken);

        var userDtos = users.Select(u => new SearchUserResponseDto(u.Id, u.UserName, u.FirstName, u.LastName,
            u.UserRoles.FirstOrDefault()?.Role?.Id,
            u.UserRoles.FirstOrDefault()?.Role?.Title,
            u.Email, u.PhoneNumber, u.IsActive, u.IsActiveDirectoryUser))
            .ToList();

        return new(request.Skip, request.Take, totalCount, userDtos);
    }
}
