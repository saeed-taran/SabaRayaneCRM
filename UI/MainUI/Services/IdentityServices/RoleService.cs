using Taran.UI.Main.Services.CurrencyServices;
using Taran.Identity.Access;
using Taran.Identity.Dtos.Roles.RoleAccesses;
using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.UI.ConfigurationModels;
using Taran.Shared.UI.Services;
using Taran.Shared.UI.Services.HttpService;

namespace Taran.UI.Main.Services.IdentityServices;

public class RoleService : ServiceBase, IRoleService
{
    private CurrentUserAccessesResponseDto? _userRoleAccesses;

    public RoleService(NavigationConfiguration navConfig, IHttpService httpService, IToastService toastService) : base(toastService, navConfig.AuthApi + "/roles", httpService)
    {

    }

    public async Task<bool> RefreshAccesses()
    {
        if (_userRoleAccesses is not null && _userRoleAccesses.AccessCodes.Count > 0)
            return false;

        var response = await httpService.Get<RequestWithUserDtoBase, CurrentUserAccessesResponseDto>(
            baseUrl + "/myAccesses",
            new() { });

        if (response.Success && response.Data is not null)
            _userRoleAccesses = response.Data;
        else
            _userRoleAccesses = new CurrentUserAccessesResponseDto(new HashSet<int>());

        return true;
    }

    public bool HaveAccess(params AccessNames[] accesses)
    {
        if (_userRoleAccesses is null)
            return false;

        return _userRoleAccesses.AccessCodes.Any(a => accesses.Any(acc => acc == (AccessNames)a));
    }

    #region Role services
    public async Task<BackendResponse<PaginatedResponseDto<SearchRoleResponseDto>>> SearchRole(SearchRoleRequestDto searchRequest)
    {
        return await httpService.Get<SearchRoleRequestDto, PaginatedResponseDto<SearchRoleResponseDto>>(
            baseUrl,
            searchRequest
        );
    }

    public async Task<BackendResponse<bool?>> CreateRole(CreateRoleRequestDto createRequestDto)
    {
        return await httpService.Post<CreateRoleRequestDto, bool?>(
            baseUrl,
            createRequestDto
        );
    }

    public async Task<BackendResponse<bool?>> UpdateRole(UpdateRoleRequestDto updateRequestDto)
    {
        return await httpService.Put<UpdateRoleRequestDto, bool?>(
            baseUrl,
            updateRequestDto
        );
    }

    public async Task<BackendResponse<LoadRoleResponseDto>> LoadRole(int id)
    {
        return await httpService.Get<LoadRoleResponseDto>(
            baseUrl + $"/{id}"
        );
    }

    public async Task<BackendResponse<bool?>> DeleteRole(int id)
    {
        return await httpService.Delete<bool?>(
            baseUrl + $"/{id}"
        );
    }

    public async Task<List<(int, string)>> GetRoleDropDownItems(int skip, int take, string term)
    {
        var roles = await SearchRole(new SearchRoleRequestDto { Skip = skip, Take = take, Term = term });
        return _GetDropDownItems(roles, c => (c.Id, c.Title));
    }
    #endregion

    #region access services
    public async Task<BackendResponse<bool?>> SetAccesses(UpdateRoleAccessRequestDto updateRoleAccessRequestDto)
    {
        return await httpService.Put<UpdateRoleAccessRequestDto, bool?>(
            baseUrl + "/accesses",
            updateRoleAccessRequestDto
        );
    }
    #endregion
}
