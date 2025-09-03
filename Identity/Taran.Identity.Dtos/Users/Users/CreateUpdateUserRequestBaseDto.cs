using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;

namespace Taran.Identity.Dtos.Users.Users;

public record CreateUpdateUserRequestBaseDto : RequestWithUserDtoBase
{
    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    [StringLength(32, ErrorMessage = nameof(KeyWords.MaximumLengthExceeded))]
    public string UserName { get; set; }

    [StringLength(32, ErrorMessage = nameof(KeyWords.MaximumLengthExceeded))]
    public string? FirstName { get; set; }

    [StringLength(32, ErrorMessage = nameof(KeyWords.MaximumLengthExceeded))]
    public string? LastName { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int RoleId { get; set; }

    [StringLength(32, ErrorMessage = nameof(KeyWords.MaximumLengthExceeded))]
    public string? Email { get; set; }

    [StringLength(16, ErrorMessage = nameof(KeyWords.MaximumLengthExceeded))]
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public bool IsActiveDirectoryUser { get; set; }
}
