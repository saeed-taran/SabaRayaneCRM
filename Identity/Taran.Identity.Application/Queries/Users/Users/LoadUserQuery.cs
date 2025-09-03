using Taran.Identity.Dtos.Users.Users;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace Taran.Identity.Application.Queries.Users.Users;

public record LoadUserQuery : CommonLoadRequestDto, IQueryWithUser<LoadUserResponseDto>
{
}
