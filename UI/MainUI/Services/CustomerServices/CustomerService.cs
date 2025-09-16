using SabaRayane.Contract.Dtos.s.Customers;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.UI.ConfigurationModels;
using Taran.Shared.UI.Services;
using Taran.Shared.UI.Services.HttpService;
using Taran.UI.Main.Services.CurrencyServices;

namespace Taran.UI.Main.Services.CustomerServices
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        public CustomerService(NavigationConfiguration navConfig, IHttpService httpService, IToastService toastService)
            : base(toastService, navConfig.ContractApi + "/customer", httpService)
        {

        }


        #region Customer services
        public async Task<BackendResponse<PaginatedResponseDto<SearchCustomerResponseDto>>> SearchCustomer(SearchCustomerRequestDto searchRequest)
        {
            return await httpService.Get<SearchCustomerRequestDto, PaginatedResponseDto<SearchCustomerResponseDto>>(
                baseUrl,
                searchRequest
            );
        }

        public async Task<BackendResponse<bool?>> CreateCustomer(CreateCustomerRequestDto createRequestDto)
        {
            return await httpService.Post<CreateCustomerRequestDto, bool?>(
                baseUrl,
                createRequestDto
            );
        }

        public async Task<BackendResponse<bool?>> UpdateCustomer(UpdateCustomerRequestDto updateRequestDto)
        {
            return await httpService.Put<UpdateCustomerRequestDto, bool?>(
                baseUrl,
                updateRequestDto
            );
        }

        public async Task<BackendResponse<bool?>> DeleteCustomer(int id)
        {
            return await httpService.Delete<bool?>(
                baseUrl + $"/{id}"
            );
        }

        public async Task<List<(int, string)>> GetCustomerDropDownItems(int skip, int take, string term)
        {
            var customers = await SearchCustomer(new SearchCustomerRequestDto { Skip = skip, Take = take });
            return _GetDropDownItems(customers, c => (c.Id, c.FullName));
        }
        #endregion

    }
}
