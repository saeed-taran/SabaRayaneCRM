
namespace Taran.Shared.UI.Services
{
    public interface ICookieStorageService
    {
        Task<T> GetValueAsync<T>(string key);
    }
}