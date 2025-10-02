using Microsoft.Extensions.Options;
using Taran.Shared.Language;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Taran.Shared.Languages;

public class TranslatorForBackend : ITranslator
{
    private readonly Dictionary<string, Dictionary<string, string>> _translations = new();
    private readonly string _language;
    public Language CurrentLanguage { get; private set; } = Language.EnglishUs;

    public TranslatorForBackend(CultureConfiguration cultureConfiguration, string resourcePath)
    {
        if(cultureConfiguration is null)
            throw new ArgumentNullException(nameof(CultureConfiguration));

        CurrentLanguage = Language.GetByName(cultureConfiguration.Language) 
            ?? throw new Exception("Language name in CultureConfiguration is invalid");

        _language = CurrentLanguage.Name;

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var files = Directory.GetFiles(resourcePath, "*.yml");

        foreach (var file in files)
        {
            var language = Path.GetFileNameWithoutExtension(file);
            var content = File.ReadAllText(file);
            var dict = deserializer.Deserialize<Dictionary<string, string>>(content);

            if (dict != null)
                _translations[language] = dict;
        }
    }

    public string Translate(string keyWord)
    {
        if (string.IsNullOrWhiteSpace(keyWord))
            return keyWord;

        if (_translations.TryGetValue(_language, out var langDict))
        {
            return langDict.TryGetValue(keyWord, out var value) ? value : keyWord;
        }

        return keyWord;
    }
}