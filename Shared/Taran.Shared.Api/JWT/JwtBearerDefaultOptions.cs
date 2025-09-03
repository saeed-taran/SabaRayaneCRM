using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Taran.Shared.Api.JWT
{
    public class JwtBearerDefaultOptions
    {
        private readonly string JWTKey;

        public JwtBearerDefaultOptions(string jwtKey)
        {
            JWTKey = jwtKey;
        }

        public void SetOptions(JwtBearerOptions options)
        {
            var Key = Encoding.UTF8.GetBytes(JWTKey);
            options.SaveToken = true;
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    return OnMessageReceived(context);
                },
                OnAuthenticationFailed = context =>
                {
                    return OnAuthenticationFailed(context);
                },
                OnTokenValidated = context =>
                {
                    return OnTokenValidated(context);
                }
            };
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
            };
        }

        protected Task OnTokenValidated(TokenValidatedContext context)
        {
            return Task.CompletedTask;
        }

        protected Task OnMessageReceived(MessageReceivedContext context)
        {
            context.Token = context.HttpContext.Request.Cookies["token"];
            if (string.IsNullOrWhiteSpace(context.Token))
                context.Token = context.HttpContext.Request.Headers["Authorization"];

            return Task.CompletedTask;
        }

        protected Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            context.HttpContext.Response.Cookies.Delete("token");
            return Task.CompletedTask;
        }
    }
}
