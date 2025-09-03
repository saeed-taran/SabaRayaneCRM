using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;

namespace Taran.Identity.Dtos.Roles.Roles;

public record UpdateRoleRequestDto : CreateRoleRequestDto
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.IdIsMissing))]
    public int Id { get; set; }
}
