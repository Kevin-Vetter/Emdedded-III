using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Content;

namespace ClimateSenseMAUI
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter([Intent.ActionView],
                  Categories = [
                Intent.CategoryDefault,
                Intent.CategoryBrowsable
                  ],
                  DataScheme = CALLBACK_SCHEME)]
    public class WebAuthenticationCallbackActivity : Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
    {
        const string CALLBACK_SCHEME = "myapp";
    }
}
