using Taran.Identity.Dtos.Users.Users;
using Taran.Shared.Application.Commands;

namespace Taran.Identity.Application.Commands.Users.Users;

public record CreateUserCommand : CreateUserRequestDto, ICommandWithUser<bool>
{
}
