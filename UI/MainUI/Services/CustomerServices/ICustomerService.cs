using SabaRayane.Contract.Dtos.s.Customers;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;

namespace Taran.UI.Main.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<BackendResponse<bool?>> CreateCustomer(CreateCustomerRequestDto createRequestDto);
        Task<BackendResponse<bool?>> DeleteCustomer(int id);
        Task<List<(int, string)>> GetCustomerDropDownItems(int skip, int take, string term);
        Task<BackendResponse<PaginatedResponseDto<SearchCustomerResponseDto>>> SearchCustomer(SearchCustomerRequestDto searchRequest);
        Task<BackendResponse<bool?>> UpdateCustomer(UpdateCustomerRequestDto updateRequestDto);
    }
}