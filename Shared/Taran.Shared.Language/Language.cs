namespace Taran.Shared.Languages;

public class Language
{
    public string Name { get; private set; }
    public string Title { get; private set; }
    public bool IsLeftToRight { get; private set; }

    private Language(string name, string title, bool isLeftToRight)
    {
        Name = name;
        Title = title;
        IsLeftToRight = isLeftToRight;
    }

    static Language() 
    {
        EnglishUs = new("en-US", "English", true);
        FarsiIR = new("fa-IR", "فارسی", false);

        _Languages = new List<Language> { EnglishUs, FarsiIR };
    }

    public readonly static Language EnglishUs;
    public readonly static Language FarsiIR;

    private static List<Language> _Languages;

    public static Language? GetByName(string name) => _Languages.FirstOrDefault(l => l.Name == name);
}
