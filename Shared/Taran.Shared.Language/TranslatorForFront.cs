using AKSoftware.Localization.MultiLanguages;
using Taran.Shared.Language;

namespace Taran.Shared.Languages;

public class TranslatorForFront : ITranslator
{
    private readonly ILanguageContainerService languageService;
    public Language CurrentLanguage { get; private set; } = Language.EnglishUs;

    public TranslatorForFront(ILanguageContainerService languageService, CultureConfiguration cultureConfiguration)
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
        if (string.IsNullOrWhiteSpace(keyWord))
            return keyWord;

        return languageService.Keys[keyWord];
    }
}