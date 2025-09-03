using Taran.Shared.Dtos;

namespace Taran.Identity.Dtos.Roles.Roles;

public record SearchRoleRequestDto : FilterRequestWithUserDtoBase
{
    public string? Term { get; set; }
}