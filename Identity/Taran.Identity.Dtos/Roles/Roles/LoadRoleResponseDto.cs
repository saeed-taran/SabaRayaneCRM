namespace Taran.Identity.Dtos.Roles.Roles;

public record LoadRoleResponseDto
{
    public LoadRoleResponseDto(int id, string name, string? title, List<int> accesses)
    {
        Id = id;
        Name = name;
        Title = title;
        Accesses = accesses;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Title { get; set; }
    public List<int> Accesses { get; set; }
}
