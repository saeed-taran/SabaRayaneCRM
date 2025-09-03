using Microsoft.JSInterop;

namespace Taran.Shared.UI.Services;

public class CookieStorageService : ICookieStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public CookieStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T> GetValueAsync<T>(string key)
    {
        var jsFunction = "const value = `; ${document.cookie}`;  const parts = value.split(`; " + key + "=`);  if (parts.length === 2) parts.pop().split(';').shift();";
        var result = await _jsRuntime.InvokeAsync<T>("eval", jsFunction);

        return result;
    }
}