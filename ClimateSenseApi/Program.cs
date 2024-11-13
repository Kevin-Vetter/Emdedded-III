using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using ClimateSenseApi.BackgroundServices;
using ClimateSenseApi.Models;
using ClimateSenseApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration);
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
builder.Services.AddSingleton<IInfluxDbService, InfluxDbService>();
builder.Services.AddHostedService<TelemetryWorkerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();