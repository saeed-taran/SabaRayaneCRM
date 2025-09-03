using Taran.Shared.Dtos;

namespace Taran.Identity.Dtos.Users.Users;

public record SearchUserRequestDto : FilterRequestWithUserDtoBase
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsActiveDirectoryUser { get; set; }
}
