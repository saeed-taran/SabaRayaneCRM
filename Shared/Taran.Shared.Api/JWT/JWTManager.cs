using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Taran.Shared.Api.Configurations;
using Taran.Shared.Core.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Taran.Shared.Api.JWT;
public class JWTManager : IJWTManager
{
    private string Key;
    private int ExpirationTimeMinutes;
    private readonly IHttpContextAccessor Context;

    public JWTManager(IHttpContextAccessor context, IOptions<IdentityConfiguration> jwtConfiguration)
    {
        Key = jwtConfiguration.Value.JWTKey;
        ExpirationTimeMinutes = jwtConfiguration.Value.JWTExpirationTimeMinutes;
        Context = context;
    }

    public string GenerateToken(IAppUser command)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(nameof(IAppUser.UserName), command.UserName.ToString()),
                new Claim(nameof(IAppUser.FirstName), command.FirstName == null ? "" : command.FirstName),
                new Claim(nameof(IAppUser.LastName), command.LastName == null ? "" : command.LastName),
                new Claim(nameof(IAppUser.UserID), command.UserID.ToString()),
                new Claim(nameof(IAppUser.LoginDate), command.LoginDate.ToString()),

                new Claim(nameof(IAppUser.RoleIds), JsonConvert.SerializeObject(command.RoleIds)),
            }),
            Expires = DateTime.UtcNow.AddMinutes(ExpirationTimeMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public void DeleteTokenCookie(string domain)
    {
        Context.HttpContext.Response.Cookies.Append("token", "",
            new CookieOptions
            {
                Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                Domain = domain
            }
        );
    }

    public string GetToken()
    {
        return Context.HttpContext.Request.Cookies["token"];
    }

    public void AddTokenCookie(string token, string domain)
    {
        Context.HttpContext.Response.Cookies.Append("token", token,
            new CookieOptions
            {
                //HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(ExpirationTimeMinutes),
                Domain = domain,
                IsEssential = true
            }
        );
    }
}