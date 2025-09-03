
namespace Taran.Shared.UI.Services
{
    public interface ILocalStorageService
    {
        Task<T?> GetItemAsync<T>(string key);
        Task RemoveItemAsync(string key);
        Task SetItem<T>(string key, T value);
    }
}