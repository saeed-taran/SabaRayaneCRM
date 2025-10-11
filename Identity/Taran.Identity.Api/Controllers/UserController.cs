using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Taran.Identity.Access;
using Taran.Identity.Application.Commands.Users.Users;
using Taran.Identity.Application.Queries.Users.Users;
using Taran.Shared.Api.Attributes;
using Taran.Shared.Api.Configurations;
using Taran.Shared.Api.Controllers;
using Taran.Shared.Api.JWT;
using Taran.Shared.Core.User;

namespace Taran.Identity.Api.Controllers
{
    public class UserController : AuthorizedControllerBase
    {
        public UserController(IMediator mediator, IAppUser appUser, IHttpContextAccessor httpContextAccessor, IJWTManager jWTManager, IOptions<IdentityConfiguration> identityConfiguration) : base(mediator, appUser, httpContextAccessor)
        {
        }

        #region User actions
        [HttpGet]
        [CustomeAuthorize((int)AccessNames.User_Get)]
        public async Task<ActionResult> Get([FromQuery] SearchUserQuery searchUserQuery)
        {
            return Ok(await SendQuery(searchUserQuery));
        }

        [HttpPost]
        [CustomeAuthorize((int)AccessNames.User_Create)]
        public async Task<ActionResult> Create([FromBody] CreateUserCommand createUserCommand)
        {
            return Ok(await SendCommand(createUserCommand));
        }

        [HttpPut]
        [CustomeAuthorize((int)AccessNames.User_Update)]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            return Ok(await SendCommand(updateUserCommand));
        }
        #endregion
    }
}
