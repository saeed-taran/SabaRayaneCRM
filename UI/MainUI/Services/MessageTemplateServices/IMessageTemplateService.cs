using SabaRayane.Contract.Dtos.MessageTemplates.PlaceHolders;
using SabaRayane.Contract.Dtos.s.MessageTemplates;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;

namespace Taran.UI.Main.Services.MessageTemplateServices
{
    public interface IMessageTemplateService
    {
        Task<BackendResponse<bool?>> CreateMessageTemplate(CreateMessageTemplateRequestDto createRequestDto);
        Task<BackendResponse<bool?>> DeleteMessageTemplate(int id);
        Task<List<(int, string)>> GetMessageTemplateDropDownItems(int skip, int take, string term);
        Task<BackendResponse<PaginatedResponseDto<SearchMessageTemplateResponseDto>>> SearchMessageTemplate(SearchMessageTemplateRequestDto searchRequest);
        Task<List<(string, string)>> GetPlaceHolderDropDownItems();
        Task<BackendResponse<bool?>> UpdateMessageTemplate(UpdateMessageTemplateRequestDto updateRequestDto);
    }
}