using Taran.UI.Main.Services.CurrencyServices;
using Taran.Identity.Dtos.Users.Users;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.UI.ConfigurationModels;
using Taran.Shared.UI.Services;
using Taran.Shared.UI.Services.HttpService;

namespace Taran.UI.Main.Services.IdentityServices
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(NavigationConfiguration navConfig, IHttpService httpService, IToastService toastService) 
            : base(toastService, navConfig.AuthApi + "/user", httpService)
        {
        }

        #region User services
        public async Task<BackendResponse<PaginatedResponseDto<SearchUserResponseDto>>> SearchUser(SearchUserRequestDto searchRequest)
        {
            return await httpService.Get<SearchUserRequestDto, PaginatedResponseDto<SearchUserResponseDto>>(
                baseUrl,
                searchRequest
            );
        }

        public async Task<BackendResponse<bool?>> CreateUser(CreateUserRequestDto createRequestDto)
        {
            return await httpService.Post<CreateUserRequestDto, bool?>(
                baseUrl,
                createRequestDto
            );
        }

        public async Task<BackendResponse<bool?>> UpdateUser(UpdateUserRequestDto updateRequestDto)
        {
            return await httpService.Put<UpdateUserRequestDto, bool?>(
                baseUrl,
                updateRequestDto
            );
        }

        public async Task<BackendResponse<bool?>> DeleteUser(int id)
        {
            return await httpService.Delete<bool?>(
                baseUrl + $"/{id}"
            );
        }

        public async Task<BackendResponse<LoadUserResponseDto>> LoadUser(int userId)
        {
            return await httpService.Get<LoadUserResponseDto>(
                baseUrl + $"/{userId}"
            );
        }
        #endregion
    }
}
