using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;

namespace Taran.Identity.Dtos.Roles.Roles;

public record CreateRoleRequestDto : RequestWithUserDtoBase
{
    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string Name { get; set; }
    public string? Title { get; set; }
}
