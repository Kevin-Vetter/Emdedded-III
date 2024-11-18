using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using ClimateSenseApi.BackgroundServices;
using ClimateSenseApi.Contexts;
using ClimateSenseApi.Models;
using ClimateSenseApi.Repositories;
using ClimateSenseServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration);

var auth0 = builder.Configuration.GetSection("Auth0");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.Authority = auth0["Domain"];
              options.Audience = auth0["Audience"];

              options.Events = new JwtBearerEvents
              {
                  OnChallenge = context =>
                  {
                      context.Response.OnStarting(async () =>
                      {
                          await context.Response.WriteAsync(
                              JsonSerializer.Serialize(new { output = "You are not authorized!" }));
                      });

                      return Task.CompletedTask;
                  }
              };
          });


builder.Services.AddDbContext<MeasurementContext>(x => x.UseSqlite("Name=Measurement"));
builder.Services.AddSingleton<MqttFactory>();
builder.Services.AddSingleton<IMqttClient>(serviceProvider =>
{
    MqttFactory mqttFactory = serviceProvider.GetRequiredService<MqttFactory>();
    IMqttClient mqttClient = mqttFactory.CreateMqttClient();
    return mqttClient;
});
builder.Services.AddSingleton<MqttClientOptionsBuilder>(serviceProvider =>
{
    IOptions<AppSettings> options = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
    return new MqttClientOptionsBuilder()
        .WithTcpServer(options.Value.MqttBroker.Host)
        .WithCredentials(options.Value.MqttBroker.Username, options.Value.MqttBroker.Password)
        .WithTlsOptions(x => x.UseTls())
        .WithProtocolVersion(MqttProtocolVersion.V311)
        .WithWillTopic("health")
        .WithWillPayload("dead")
        .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
        .WithWillRetain();
});
builder.Services.AddSingleton<IMqttService, MqttService>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddHostedService<MeasurementWorkerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();