using Microsoft.JSInterop;

namespace Taran.Shared.UI.Services;

public class ToastService : IToastService
{
    private readonly IJSRuntime _jsRuntime;

    public ToastService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task Show(string message, ToastKind toastKind)
    {
        await _jsRuntime.InvokeVoidAsync("showToast", message, "bg-" + toastKind.ToString());
    }
}