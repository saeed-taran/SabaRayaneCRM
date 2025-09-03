using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Application.Commands;

namespace Taran.Identity.Application.Commands.Roles;

public record UpdateRoleCommand : UpdateRoleRequestDto, ICommandWithUser<bool>
{
}