using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;

namespace Taran.Identity.Dtos.Users.Users;

public record CreateUserRequestDto : CreateUpdateUserRequestBaseDto
{
    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    [StringLength(32, MinimumLength = 6, ErrorMessage = nameof(KeyWords.PasswordLengthDoesNotMatch))]
    public string Password { get; set; }
}
