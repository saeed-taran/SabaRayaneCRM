using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;

namespace Taran.Identity.Dtos.Users.Users;

public record UpdateUserRequestDto : CreateUpdateUserRequestBaseDto
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.IdIsMissing))]
    public int Id { get; set; }
}
