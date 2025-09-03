namespace Taran.Identity.Dtos.Roles.Roles;

public record SearchRoleResponseDto
{
    public SearchRoleResponseDto(int id, string name, string? title)
    {
        Id = id;
        Name = name;
        Title = title;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Title { get; private set; }
}
