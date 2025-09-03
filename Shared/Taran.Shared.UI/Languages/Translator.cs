using AKSoftware.Localization.MultiLanguages;
using Taran.Shared.Dtos.ConfigurationModels;

namespace Taran.Shared.UI.Languages;

public class Translator : ITranslator
{
    private readonly ILanguageContainerService languageService;
    public Language CurrentLanguage { get; private set; } = Language.EnglishUs;

    public Translator(ILanguageContainerService languageService, CultureConfiguration cultureConfiguration)
    {
        this.languageService = languageService;

        if(cultureConfiguration is null)
            throw new ArgumentNullException(nameof(CultureConfiguration));

        CurrentLanguage = Language.GetByName(cultureConfiguration.Language) 
            ?? throw new Exception("Language name in CultureConfiguration is invalid");

        languageService.SetLanguage(System.Globalization.CultureInfo.GetCultureInfo(CurrentLanguage.Name));
    }

    public string Translate(string keyWord)
    {
        return languageService.Keys[keyWord];
    }
}