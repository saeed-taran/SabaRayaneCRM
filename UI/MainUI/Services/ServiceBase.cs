using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.UI.Services;
using Taran.Shared.UI.Services.HttpService;

namespace Taran.UI.Main.Services.CurrencyServices;

public class ServiceBase
{
    private readonly IToastService toastService;
    protected readonly string baseUrl;
    protected readonly IHttpService httpService;

    public ServiceBase(IToastService toastService, string baseUrl, IHttpService httpService)
    {
        this.toastService = toastService;
        this.baseUrl = baseUrl;
        this.httpService = httpService;
    }

    public void ShowToastMessage(string? message, ToastKind toastKind)
    {
        if (message is null)
            return;

        toastService.Show(message, toastKind);
    }

    protected List<(int, string)> _GetDropDownItems<Response>(BackendResponse<PaginatedResponseDto<Response>> backendResponse,
        Func<Response, (int, string)> selector)
    {
        if (!backendResponse.Success)
        {
            ShowToastMessage(backendResponse.ErrorMessage, ToastKind.danger);
            return new();
        }

        var dropDownItems = backendResponse.Data!.Results.Select(selector).ToList();
        return dropDownItems;
    }

    protected List<(long, string)> _GetDropDownItems<Response>(BackendResponse<PaginatedResponseDto<Response>> backendResponse,
        Func<Response, (long, string)> selector)
    {
        if (!backendResponse.Success)
        {
            ShowToastMessage(backendResponse.ErrorMessage, ToastKind.danger);
            return new();
        }

        var dropDownItems = backendResponse.Data!.Results.Select(selector).ToList();
        return dropDownItems;
    }
}