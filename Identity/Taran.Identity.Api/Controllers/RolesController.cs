using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taran.Identity.Access;
using Taran.Identity.Application.Commands.RoleAccesses;
using Taran.Identity.Application.Commands.Roles;
using Taran.Identity.Application.Queries.Roles;
using Taran.Identity.Dtos.Roles.Roles;
using Taran.Shared.Api.Attributes;
using Taran.Shared.Api.Controllers;
using Taran.Shared.Core.User;

namespace Taran.Identity.Api.Controllers
{
    public class RolesController : AuthorizedControllerBase
    {
        public RolesController(IMediator mediator, ILogger<RolesController> logger, IAppUser appUser, IHttpContextAccessor httpContextAccessor) 
            : base(mediator, appUser, httpContextAccessor)
        {
        }

        [HttpGet]
        [CustomeAuthorize((int)AccessNames.Roles_Get)]
        public async Task<ActionResult> Get([FromQuery] SearchRoleQuery searchRoleQuery)
        {
            return Ok(await SendQuery(searchRoleQuery));
        }


        [HttpGet("myAccesses")]
        public async Task<ActionResult<CurrentUserAccessesResponseDto>> GetCurrentUserAccesses()
        {
            var query = new CurrentUserAccessesQuery();
            return Ok(await SendQuery(query));
        }

        [HttpPost]
        [CustomeAuthorize((int)AccessNames.Roles_Create)]
        public async Task<ActionResult> Create([FromBody] CreateRoleCommand createRoleCommand)
        {
            return Ok(await SendCommand(createRoleCommand));
        }

        [HttpPut]
        [CustomeAuthorize((int)AccessNames.Roles_Update)]
        public async Task<ActionResult> Update([FromBody] UpdateRoleCommand updateRoleCommand)
        {
            return Ok(await SendCommand(updateRoleCommand));
        }

        [HttpDelete("{Id}")]
        [CustomeAuthorize((int)AccessNames.Roles_Delete)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await SendCommand(new DeleteRoleCommand { Id = id }));
        }

        [HttpGet("{Id}")]
        [CustomeAuthorize((int)AccessNames.Roles_Update)]
        public async Task<ActionResult<IEnumerable<LoadRoleResponseDto>>> Load([FromRoute] LoadRoleQuery loadRoleQuery)
        {
            return Ok(await SendQuery(loadRoleQuery));
        }

        [AllowAnonymous]
        [HttpGet("accesses")]
        public async Task<ActionResult> Get()
        {
            GetAllRolesAccessesQuery getAllRolesAccessesQuery = new();
            var result = await SendInternalQuery(getAllRolesAccessesQuery);

            return Ok(result.RoleAccesses);
        }

        [HttpPut("accesses")]
        public async Task<ActionResult> SetAccesses([FromBody] UpdateRoleAccessCommand updateRoleAccessCommand)
        {
            return Ok(await SendCommand(updateRoleAccessCommand));

        }
    }
}