using ClimateSenseMAUI.View;
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
            AppContext.SetSwitch("System.Reflection.NullabilityInfoContext.IsSupported", true);

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = Appsettings.Auth0["Domain"],
                ClientId = Appsettings.Auth0["ClientId"],
                RedirectUri = Appsettings.Auth0["RedirectUri"],
                PostLogoutRedirectUri = Appsettings.Auth0["PostLogoutRedirectUri"],
                Scope = Appsettings.Auth0["Scope"]
            }));


            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
