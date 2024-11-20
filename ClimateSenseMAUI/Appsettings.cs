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
            { "RedirectUri", "myapp://callback"},
            { "PostLogoutRedirectUri", "myapp://callback" },
            { "Scope", "openid profile email" },
        };

        public static Dictionary<string, string> MqttBroker = new()
        {
            {"Host", "6d50cc68ea9d4e079719910a30d98aee.s1.eu.hivemq.cloud"},
            {"Username", "dotnet"},
            {"Password", "haSGlWNJemJOVTpR"},
        };
    }
}
