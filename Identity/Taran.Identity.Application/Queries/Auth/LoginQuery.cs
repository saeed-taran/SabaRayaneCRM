using Taran.Identity.Dtos.Auth;
using Taran.Shared.Application.Queries;

namespace Taran.Identity.Application.Queries.Auth;

public record LoginQuery : LoginRequestDto, IQueryWithoutUser<LoginUserResponseDto>
{

}