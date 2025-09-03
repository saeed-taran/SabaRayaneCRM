using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;

namespace Taran.Identity.Dtos.Roles.RoleAccesses;

public record UpdateRoleAccessRequestDto : RequestWithUserDtoBase
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.IdIsMissing))]
    public int RoleId { get; set; }
    [Length(1, 5000, ErrorMessage = nameof(KeyWords.MaximumLengthExceeded))]
    public int[] AccessIds { get; set; } = new int[0];
}
