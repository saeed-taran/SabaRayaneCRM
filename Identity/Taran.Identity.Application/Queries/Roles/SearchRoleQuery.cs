using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace Taran.Identity.Application.Queries.Roles;

public record SearchRoleQuery : SearchRoleRequestDto, IQueryWithUser<PaginatedResponseDto<SearchRoleResponseDto>>
{

}