using Microsoft.AspNetCore.Components.Authorization;
using Taran.Shared.UI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Taran.Shared.UI.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private ICookieStorageService _cookieStorageService;
    public User user { get; private set; }

    public AuthStateProvider(HttpClient httpClient, ICookieStorageService cookieStorageService)
    {
        _cookieStorageService = cookieStorageService ?? throw new ArgumentNullException(nameof(cookieStorageService));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _cookieStorageService.GetValueAsync<string>("token");

        if (token == null || token == "")
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                identity = new(jwtSecurityToken.Claims, nameof(AuthStateProvider));

                user = User.FromClaimsPrincipal(new ClaimsPrincipal(identity));
            }

            return new AuthenticationState(new(identity));
        }
        catch (Exception ex)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}