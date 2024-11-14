using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateSenseMAUI
{
    public static class Appsettings
    {
        public static Dictionary<string, string> Auth0 = new()
        {
            { "Domain", "dev-dpa8tyoky8r1sgd3.us.auth0.com" },
            { "ClientId", "KkEXTxrvVtvqnD2HYtOFss2NP1xf7rbD"},
            { "RedirectUri", "myapp://callback/"},
            { "PostLogoutRedirectUri", "myapp://callback/" },
            { "Scope", "openid profile email" },
        };
    }
}
