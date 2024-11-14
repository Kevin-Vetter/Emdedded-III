using ClimateSenseMAUI.View;
using ClimateSenseMAUI.ViewModel;
using ClimateSenseService;
using Microsoft.Extensions.Logging;
using Auth0.OidcClient;
using Syncfusion.Maui.Core.Hosting;

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
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-dpa8tyoky8r1sgd3.us.auth0.com",
                ClientId = "KkEXTxrvVtvqnD2HYtOFss2NP1xf7rbD",
                RedirectUri = "myapp://callback/",
                PostLogoutRedirectUri = "myapp://callback/",
                Scope = "openid profile email"
            }));

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IRoomService, RoomService>();
            
            builder.Services.AddTransient<RoomDetailPage>(); 
            builder.Services.AddTransient<RoomDetailViewModel>();

            return builder.Build();
        }
    }
}
