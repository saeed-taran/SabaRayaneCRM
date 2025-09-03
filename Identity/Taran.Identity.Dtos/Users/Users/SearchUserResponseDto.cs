namespace Taran.Identity.Dtos.Users.Users;

public record SearchUserResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? RoleTitle { get; set; }
    public int? RoleId { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public bool IsActiveDirectoryUser { get; set; }

    public SearchUserResponseDto() { }

    public SearchUserResponseDto(int id, string userName, string? firstName, string? lastName, int? roleId, string? roleTitle, string? email, string? phoneNumber, bool isActive, bool isActiveDirectoryUser)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        RoleTitle = roleTitle;
        RoleId = roleId;
        Email = email;
        PhoneNumber = phoneNumber;
        IsActive = isActive;
        IsActiveDirectoryUser = isActiveDirectoryUser;
    }

}
