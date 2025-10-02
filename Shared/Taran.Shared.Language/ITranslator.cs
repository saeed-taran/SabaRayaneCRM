
namespace Taran.Shared.Languages
{
    public interface ITranslator
    {
        string Translate(string keyWord);
        Language CurrentLanguage { get; }
    }
}