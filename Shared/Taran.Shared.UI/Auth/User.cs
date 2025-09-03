using Newtonsoft.Json;
using System.Security.Claims;

namespace Taran.Shared.UI.Auth;

public class User
{
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public IReadOnlyCollection<int> RoleIds { get; init; }

    public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
    {
        new (ClaimTypes.Name, UserName),
        new (nameof(FirstName), FirstName),
        new (nameof(LastName), LastName),
    }.Concat(RoleIds.Select(r => new Claim(ClaimTypes.Role, r.ToString())).ToArray()),
    nameof(AuthStateProvider)));

    public static User FromClaimsPrincipal(ClaimsPrincipal principal) 
    {
        User user = new()
        {
            UserName = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            FirstName = principal.FindFirst(nameof(FirstName))?.Value ?? "",
            LastName = principal.FindFirst(nameof(LastName))?.Value ?? "",
            RoleIds = principal.FindAll(ClaimTypes.Role).Select(c => Convert.ToInt32(c.Value)).ToList(),
        };

        return user;
    }
}
