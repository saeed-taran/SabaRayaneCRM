namespace Taran.Identity.Dtos.Roles.Roles;

public record GetAllRolesAccessesResponseDto
{
    public GetAllRolesAccessesResponseDto(IReadOnlyDictionary<int, HashSet<int>> roleAccesses)
    {
        RoleAccesses = roleAccesses;
    }

    public IReadOnlyDictionary<int, HashSet<int>> RoleAccesses { get; private set; }
}
