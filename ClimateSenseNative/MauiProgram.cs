using System.Security.Authentication;
using Auth0.OidcClient;
using ClimateSenseServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using Radzen;

namespace ClimateSenseNative;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = Appsettings.Auth0["Domain"],
            ClientId = Appsettings.Auth0["ClientId"],
            RedirectUri = Appsettings.Auth0["RedirectUri"],
            PostLogoutRedirectUri = Appsettings.Auth0["PostLogoutRedirectUri"],
            Scope = Appsettings.Auth0["Scope"],
        }));

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddSingleton<TokenHandler>();
        
        builder.Services.AddHttpClient("ClimateSenseApi",
            client => client.BaseAddress = new Uri(Appsettings.ClimateSenseApi["Host"])
        ).AddHttpMessageHandler<TokenHandler>();

        builder.Services.AddTransient(
            sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ClimateSenseApi")
        );

        builder.Services.AddSingleton(serviceProvider =>
        {
            return new MqttClientOptionsBuilder()
                .WithTcpServer(Appsettings.MqttBroker["Host"])
                .WithCredentials(Appsettings.MqttBroker["Username"], Appsettings.MqttBroker["Password"])
                .WithTlsOptions(x =>
                {
                    x.UseTls();
                    x.WithSslProtocols(SslProtocols.Tls12);
                    x.WithIgnoreCertificateChainErrors();
                })
                .WithProtocolVersion(MqttProtocolVersion.V311);
        });
        builder.Services.AddSingleton<MqttFactory>();
        builder.Services.AddSingleton<IMqttClient>(serviceProvider =>
        {
            MqttFactory mqttFactory = serviceProvider.GetRequiredService<MqttFactory>();
            IMqttClient mqttClient = mqttFactory.CreateMqttClient();
            return mqttClient;
        });
        builder.Services.AddSingleton<IMqttService, MqttService>();
        builder.Services.AddSingleton<IApiService, ApiService>();
        builder.Services.AddRadzenComponents();


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}