using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Auth0.AspNetCore.Authentication;
using Auth0.OidcClient;
using ClimateSenseServices;
using ClimateSenseWeb;
using ClimateSenseWeb.Components;
using ClimateSenseWeb.Middleware.HttpClientHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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

IConfigurationSection auth0 = builder.Configuration.GetSection("Auth0");
builder.Services.AddAuth0WebAppAuthentication(options =>
{

    options.Domain = auth0["Domain"]!;
    options.ClientId = auth0["ClientId"]!;
    options.CallbackPath = auth0["RedirectUri"]!;
    options.ClientSecret = auth0["ClientSecret"];
    options.Scope = "openid email profile";
    // options.OpenIdConnectEvents = new OpenIdConnectEvents()
    // {
    //     OnTokenValidated = context =>
    //     {
    //         if (context.TokenEndpointResponse == null)
    //         {
    //             return Task.CompletedTask;
    //         }
    //
    //         string accessToken = context.TokenEndpointResponse.AccessToken;
    //
    //         JwtSecurityToken securityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
    //
    //         ((ClaimsIdentity?)context.Principal?.Identity).AddClaim(new Claim(ClaimTypes.Role, ""));
    //
    //         return Task.CompletedTask;
    //     }
    // };
}).WithAccessToken(options => options.Audience = "climateSenseAPI");
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();


IConfigurationSection apiSettings = builder.Configuration.GetSection("ClimateSenseApi");
builder.Services.AddHttpClient("ClimateSenseApi", client => client.BaseAddress = new Uri(apiSettings["Host"]!))
    .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("ClimateSenseApi"));
builder.Services.AddScoped<IApiService, ApiService>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("WriteAccess", policy =>
                      policy.RequireClaim("permissions", "write:servo"))
    .AddPolicy("ReadAccess", policy =>
                      policy.RequireClaim("permissions", "read:sensor"));


builder.Services.AddSingleton(new Auth0Client(new()
{
    Domain = auth0["Domain"],
    ClientId = auth0["ClientId"],
    RedirectUri = auth0["RedirectUri"],
    PostLogoutRedirectUri = auth0["PostLogoutRedirectUri"],
    Scope = auth0["Scope"]
}));

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
    //Auth0Client client = new Auth0Client();
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