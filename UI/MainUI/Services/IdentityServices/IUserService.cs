using Taran.Identity.Dtos.Users.Users;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;

namespace Taran.UI.Main.Services.IdentityServices
{
    public interface IUserService
    {
        Task<BackendResponse<bool?>> CreateUser(CreateUserRequestDto createRequestDto);
        Task<BackendResponse<bool?>> DeleteUser(int id);
        Task<BackendResponse<LoadUserResponseDto>> LoadUser(int userId);
        Task<BackendResponse<PaginatedResponseDto<SearchUserResponseDto>>> SearchUser(SearchUserRequestDto searchRequest);
        Task<BackendResponse<bool?>> UpdateUser(UpdateUserRequestDto updateRequestDto);
    }
}