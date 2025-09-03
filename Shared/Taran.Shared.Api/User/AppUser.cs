using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Taran.Shared.Core.User;

namespace Taran.Shared.Api.User
{
    public class AppUser : IAppUser
    {
        public int UserID { get; protected set; }
        public string UserName { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public IReadOnlyCollection<int> RoleIds { get; protected set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public long LoginDate { get; protected set; }

        public AppUser(int userID, string userName, string firstName, string lastName, List<int> roleIds, long loginDate)
        {
            UserID = userID;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            RoleIds = roleIds;
            LoginDate = loginDate;
        }

        public AppUser(IHttpContextAccessor context)
        {
            if (context == null)
                return;

            if (context.HttpContext == null)
                return;

            if (!context.HttpContext.User.Identity.IsAuthenticated)
                return;

            UserID = int.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == nameof(IAppUser.UserID)).Value);
            UserName = Convert.ToString(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == nameof(IAppUser.UserName)).Value);
            FirstName = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == nameof(IAppUser.FirstName)).Value;
            LastName = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == nameof(IAppUser.LastName)).Value;            
            RoleIds = JsonConvert.DeserializeObject<List<int>>(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == nameof(IAppUser.RoleIds)).Value);
            LoginDate = long.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == nameof(IAppUser.LoginDate)).Value);
        }
    }
}