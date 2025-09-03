using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace Taran.Identity.Application.Commands.Roles;

public record DeleteRoleCommand : CommonDeleteRequestDto, ICommandWithUser<bool>
{
}
