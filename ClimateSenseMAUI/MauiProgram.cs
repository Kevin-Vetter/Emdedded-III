﻿using ClimateSenseMAUI.View;
using ClimateSenseMAUI.ViewModel;
using Microsoft.Extensions.Logging;
using Auth0.OidcClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ClimateSenseMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new Auth0Client(new Auth0ClientOptions
            {
                Domain = "dev-dpa8tyoky8r1sgd3.us.auth0.com",
                ClientId = "KkEXTxrvVtvqnD2HYtOFss2NP1xf7rbD",
                RedirectUri = "myapp://callback/",
                Scope = "openid profile"
            }));

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
