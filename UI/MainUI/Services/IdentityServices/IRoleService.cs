using Taran.Identity.Access;
using Taran.Identity.Dtos.Roles.RoleAccesses;
using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;

namespace Taran.UI.Main.Services.IdentityServices
{
    public interface IRoleService
    {
        Task<BackendResponse<bool?>> CreateRole(CreateRoleRequestDto createRequestDto);
        Task<BackendResponse<bool?>> DeleteRole(int id);
        Task<List<(int, string)>> GetRoleDropDownItems(int skip, int take, string term);
        bool HaveAccess(params AccessNames[] accesses);
        Task<BackendResponse<LoadRoleResponseDto>> LoadRole(int id);
        Task<bool> RefreshAccesses();
        Task<BackendResponse<PaginatedResponseDto<SearchRoleResponseDto>>> SearchRole(SearchRoleRequestDto searchRequest);
        Task<BackendResponse<bool?>> SetAccesses(UpdateRoleAccessRequestDto updateRoleAccessRequestDto);
        Task<BackendResponse<bool?>> UpdateRole(UpdateRoleRequestDto updateRequestDto);
    }
}