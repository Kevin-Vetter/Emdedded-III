using ClimateSenseMAUI.View;
using ClimateSenseMAUI.ViewModel;
using ClimateSenseServices;
using ClimateSenseService;
using Microsoft.Extensions.Logging;
using Auth0.OidcClient;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
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
                Domain = Appsettings.Auth0["Domain"],
                ClientId = Appsettings.Auth0["ClientId"],
                RedirectUri = Appsettings.Auth0["RedirectUri"],
                PostLogoutRedirectUri = Appsettings.Auth0["PostLogoutRedirectUri"],
                Scope = Appsettings.Auth0["Scope"]
            }));

            builder.Services.AddSingleton(serviceProvider =>
            {
                return new MqttClientOptionsBuilder()
                    .WithTcpServer(Appsettings.MqttBroker["Host"])
                    .WithCredentials(Appsettings.MqttBroker["Username"], Appsettings.MqttBroker["Password"])
                    .WithTlsOptions(x => x.UseTls())
                    .WithProtocolVersion(MqttProtocolVersion.V311)
                    .WithWillTopic("health")
                    .WithWillPayload("dead")
                    .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                    .WithWillRetain();
            });
            builder.Services.AddSingleton<MqttFactory>();
            builder.Services.AddSingleton<IMqttClient>(serviceProvider =>
            {
                MqttFactory mqttFactory = serviceProvider.GetRequiredService<MqttFactory>();
                IMqttClient mqttClient = mqttFactory.CreateMqttClient();
                return mqttClient;
            });
            builder.Services.AddSingleton<IMqttService, MqttService>();
            builder.Services.AddSingleton<NotificationViewModel>();

            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

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
