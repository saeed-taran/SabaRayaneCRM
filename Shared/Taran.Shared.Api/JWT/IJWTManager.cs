using Taran.Shared.Api.User;
using Taran.Shared.Core.User;

namespace Taran.Shared.Api.JWT;

public interface IJWTManager
{
    void DeleteTokenCookie(string domain);
    string GenerateToken(IAppUser command);
    string GetToken();
    void AddTokenCookie(string token, string domain);
}