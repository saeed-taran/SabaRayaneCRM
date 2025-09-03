namespace Taran.Identity.Dtos.Roles.Roles;

public record CurrentUserAccessesResponseDto
{
    public HashSet<int> AccessCodes { get; set; }

    public CurrentUserAccessesResponseDto(HashSet<int> accessCodes)
    {
        AccessCodes = accessCodes;
    }
}
