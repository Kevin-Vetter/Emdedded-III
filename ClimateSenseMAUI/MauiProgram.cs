using ClimateSenseMAUI.View;
using ClimateSenseMAUI.ViewModel;
using ClimateSenseServices;
using Microsoft.Extensions.Logging;
using Auth0.OidcClient;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;

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
                Domain = "dev-dpa8tyoky8r1sgd3.us.auth0.com",
                ClientId = "KkEXTxrvVtvqnD2HYtOFss2NP1xf7rbD",
                RedirectUri = "myapp://callback/",
                PostLogoutRedirectUri = "myapp://callback/",
                Scope = "openid profile email"
            }));

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
