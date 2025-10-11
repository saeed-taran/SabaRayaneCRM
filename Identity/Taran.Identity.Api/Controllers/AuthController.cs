using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Taran.Identity.Dtos.Auth;
using Taran.Identity.Application.Queries.Auth;
using Taran.Shared.Api.Configurations;
using Taran.Shared.Api.Controllers;
using Taran.Shared.Api.JWT;
using Taran.Shared.Api.User;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.Dtos.Languages;
using Taran.Shared.Core.User;

namespace Taran.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : UnAuthorizedControllerBase
    {
        private readonly IJWTManager jWTManager;
        private readonly IdentityConfiguration identityConfiguration;

        public AuthController(IMediator mediator, IOptions<IdentityConfiguration> identityConfiguration, 
            IJWTManager jWTManager, ILogger<AuthController> logger) : base(mediator)
        {
            this.identityConfiguration = identityConfiguration.Value;
            this.jWTManager = jWTManager;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            var response = await SendQuery(loginQuery);
            if (response.Data is null)
                return Ok(new ErrorResponse(nameof(KeyWords.InvalidUserNameOrPassword)));

            var user = response.Data;

            IAppUser appUser = new AppUser(user.Id, user.UserName, user.FirstName, user.LastName, user.UserRoles, DateTime.Now.Ticks);

            var token = jWTManager.GenerateToken(appUser);
            jWTManager.AddTokenCookie(token, identityConfiguration.CookieDomain);

            return Ok(new SuccessfulResponse<LoginResponseDto>(new LoginResponseDto(token)));
        }

        [HttpGet("logout")]
        public ActionResult Logout(string backUrl)
        {
            jWTManager.DeleteTokenCookie(identityConfiguration.CookieDomain);
            return Redirect(backUrl);
        }
    }
}