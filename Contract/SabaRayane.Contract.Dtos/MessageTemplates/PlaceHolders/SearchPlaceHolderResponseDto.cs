namespace SabaRayane.Contract.Dtos.MessageTemplates.PlaceHolders;

public record SearchPlaceHolderResponseDto
{
    public SearchPlaceHolderResponseDto(string name, string title)
    {
        Name = name;
        Title = title;
    }

    public string Name { get; private set; }
    public string Title { get; private set; }
}
