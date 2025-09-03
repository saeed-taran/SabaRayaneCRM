namespace Taran.Identity.Dtos.Auth;

public class LoginUserResponseDto
{
    public LoginUserResponseDto(int id, string userName, string firstName, string lastName, List<int> userRoles)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        UserRoles = userRoles;
    }

    public int Id { get; private set; }
    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public List<int> UserRoles { get; private set; }
}