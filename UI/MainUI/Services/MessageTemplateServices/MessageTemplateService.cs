using SabaRayane.Contract.Dtos.MessageTemplates.PlaceHolders;
using SabaRayane.Contract.Dtos.s.MessageTemplates;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.UI.ConfigurationModels;
using Taran.Shared.UI.Services;
using Taran.Shared.UI.Services.HttpService;
using Taran.UI.Main.Services.CurrencyServices;

namespace Taran.UI.Main.Services.MessageTemplateServices;

public class MessageTemplateService : ServiceBase, IMessageTemplateService
{
    public MessageTemplateService(NavigationConfiguration navConfig, IHttpService httpService, IToastService toastService)
        : base(toastService, navConfig.ContractApi + "/messageTemplate", httpService)
    {
    }

    #region MessageTemplate services
    public async Task<BackendResponse<PaginatedResponseDto<SearchMessageTemplateResponseDto>>> SearchMessageTemplate(SearchMessageTemplateRequestDto searchRequest)
    {
        return await httpService.Get<SearchMessageTemplateRequestDto, PaginatedResponseDto<SearchMessageTemplateResponseDto>>(
            baseUrl,
            searchRequest
        );
    }

    public async Task<BackendResponse<bool?>> CreateMessageTemplate(CreateMessageTemplateRequestDto createRequestDto)
    {
        return await httpService.Post<CreateMessageTemplateRequestDto, bool?>(
            baseUrl,
            createRequestDto
        );
    }

    public async Task<BackendResponse<bool?>> UpdateMessageTemplate(UpdateMessageTemplateRequestDto updateRequestDto)
    {
        return await httpService.Put<UpdateMessageTemplateRequestDto, bool?>(
            baseUrl,
            updateRequestDto
        );
    }

    public async Task<BackendResponse<bool?>> DeleteMessageTemplate(int id)
    {
        return await httpService.Delete<bool?>(
            baseUrl + $"/{id}"
        );
    }

    public async Task<List<(int, string)>> GetMessageTemplateDropDownItems(int skip, int take, string term)
    {
        var messageTemplates = await SearchMessageTemplate(new SearchMessageTemplateRequestDto { Skip = skip, Take = take });
        return _GetDropDownItems(messageTemplates, c => (c.Id, c.Name));
    }
    #endregion

    public async Task<List<(string, string)>> GetPlaceHolderDropDownItems()
    {
        var placeHolders = await httpService.Get<PaginatedResponseDto<SearchPlaceHolderResponseDto>>(
            baseUrl + "/PlaceHolder"
        );

        return _GetDropDownItems(placeHolders, c => (c.Title, c.Name));
    }
}