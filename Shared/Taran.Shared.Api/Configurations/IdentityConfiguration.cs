using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taran.Shared.Api.Configurations
{
    public class IdentityConfiguration
    {
        public string JWTKey { get; set; }
        public int JWTExpirationTimeMinutes { get; set; }
        public string ApiAddress { get; set; }
        public string CookieDomain { get; set; }
    }
}
