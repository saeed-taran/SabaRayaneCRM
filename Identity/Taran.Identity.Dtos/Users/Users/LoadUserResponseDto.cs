namespace Taran.Identity.Dtos.Users.Users;

public record LoadUserResponseDto
{
    public string UserName { get; set; }
    public int UserId { get; set; }
    public List<int> UserRoles { get; set; }

    public LoadUserResponseDto(int userId, string userName, List<int> userRoles)
    {
        UserId = userId;
        UserName = userName;
        UserRoles = userRoles;
    }
}
