using System.Text.Json;
using Auth0.AspNetCore.Authentication;
using ClimateSenseServices;
using ClimateSenseWeb.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using Radzen;
using Radzen.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<IApiService, ApiService>();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    IConfigurationSection auth0 = builder.Configuration.GetSection("Auth0");

    options.Domain = auth0["Domain"]!;
    options.ClientId = auth0["ClientId"]!;
    options.CallbackPath = auth0["RedirectUri"]!;
    options.Scope = "openid email profile";
});
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddSingleton<MqttFactory>();
builder.Services.AddSingleton<IMqttClient>(serviceProvider =>
{
    MqttFactory mqttFactory = serviceProvider.GetRequiredService<MqttFactory>();
    IMqttClient mqttClient = mqttFactory.CreateMqttClient();
    return mqttClient;
});
builder.Services.AddSingleton<MqttClientOptionsBuilder>(_ =>
{
    IConfigurationSection mqttSettings = builder.Configuration.GetSection("MqttSettings");
    return new MqttClientOptionsBuilder()
        .WithWebSocketServer(optionsBuilder =>
        {
            optionsBuilder.WithUri(mqttSettings["Host"]);
            optionsBuilder.WithUseDefaultCredentials();
            optionsBuilder.Build();
        })
        .WithCredentials(mqttSettings["Username"], mqttSettings["Password"])
        .WithTlsOptions(x => x.UseTls())
        .WithProtocolVersion(MqttProtocolVersion.V311);
});

builder.Services.AddSingleton<IMqttService, MqttService>();
builder.Services.AddMemoryCache();
builder.Services.AddRadzenComponents();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/Account/Login", async (HttpContext context, string returnUrl = "/") =>
{
    AuthenticationProperties authenticationProperties = new LoginAuthenticationPropertiesBuilder().WithRedirectUri(returnUrl).Build();

    await context.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext) =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        .WithRedirectUri("/")
        .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();