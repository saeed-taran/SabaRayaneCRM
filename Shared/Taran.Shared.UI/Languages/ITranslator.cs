
namespace Taran.Shared.UI.Languages
{
    public interface ITranslator
    {
        string Translate(string keyWord);
        Language CurrentLanguage { get; }
    }
}