using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Taran.Shared.Api.Attributes;
using Taran.Shared.Api.Configurations;
using Taran.Shared.Core.User;
using System.Net;

namespace Taran.Shared.Api.Middlewares
{
    public class AuthorizeMiddleware : IMiddleware
    {
        private readonly IAppUser appUser;
        private readonly IdentityConfiguration identityConfiguration;
        private IMemoryCache _memoryCache;
        private Dictionary<int, HashSet<int>> roleAccess
        {
            get
            {
                return _memoryCache.Get<Dictionary<int, HashSet<int>>>(nameof(roleAccess));
            }
            set 
            {
                _memoryCache.Set(nameof(roleAccess), value, TimeSpan.FromMinutes(1));
            }
        }

        private bool IsUserLoginDuplicated(long userId)
        {
            var key = $"{nameof(IsUserLoginDuplicated)}_{userId}";
            var loginDate = _memoryCache.Get<long?>(key);
            if (loginDate is null)
            {
                _memoryCache.Set(key, appUser.LoginDate, TimeSpan.FromMinutes(120));
                return false;
            }

            if (loginDate > appUser.LoginDate)
                return true;

            if (loginDate < appUser.LoginDate)
                _memoryCache.Set(key, appUser.LoginDate, TimeSpan.FromMinutes(120));

            return false;
        }

        public AuthorizeMiddleware(IAppUser appUser, IOptions<IdentityConfiguration> identityConfiguration, IMemoryCache memoryCache)
        {
            this.appUser = appUser;
            this.identityConfiguration = identityConfiguration.Value;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authorizeAttribute =
                (CustomeAuthorizeAttribute?)context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata
                .Where(x => x is CustomeAuthorizeAttribute).FirstOrDefault();

            if (authorizeAttribute == null)
            {
                await next(context);
                return;
            }

            var roleAccessTemp = roleAccess;

            if (roleAccessTemp == null)
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(identityConfiguration.ApiAddress + "roles/Accesses");
                    if (!response.IsSuccessStatusCode)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return;
                    }

                    string result = await response.Content.ReadAsStringAsync();

                    var resultConverted = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, HashSet<int>>>(result);
                    if(resultConverted == null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return;
                    }
                    roleAccess = resultConverted;
                    roleAccessTemp = roleAccess;
                }
            }

            foreach (var roleId in appUser.RoleIds) 
            {
                if (roleAccessTemp.ContainsKey(roleId) && roleAccessTemp[roleId].Contains(authorizeAttribute.AccessCode))
                {
                    if (IsUserLoginDuplicated(appUser.UserID))
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    else
                        await next(context);
                    return;
                }
            }

            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return;
        }
    }
}