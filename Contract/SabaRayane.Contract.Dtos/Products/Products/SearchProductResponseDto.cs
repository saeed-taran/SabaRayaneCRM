namespace SabaRayane.Contract.Dtos.s.Products;

public record SearchProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Price { get; set; }
    public string Description { get; set; }

    public SearchProductResponseDto(int id, string name, long price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}
