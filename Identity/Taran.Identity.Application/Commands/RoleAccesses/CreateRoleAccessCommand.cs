using Taran.Identity.Dtos.Roles.RoleAccesses;
using Taran.Shared.Application.Commands;

namespace Taran.Identity.Application.Commands.RoleAccesses;

public record UpdateRoleAccessCommand : UpdateRoleAccessRequestDto, ICommandWithUser<bool>
{
}
