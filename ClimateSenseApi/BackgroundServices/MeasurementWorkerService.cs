using System.ComponentModel;
using System.Text;
using System.Text.Json;
using ClimateSenseApi.Models;
using ClimateSenseApi.Repositories;
using ClimateSenseApi.Services;
using MQTTnet.Client;

namespace ClimateSenseApi.BackgroundServices;

public class MeasurementWorkerService(ILogger<MeasurementWorkerService> logger, IMqttService mqttService, IServiceScopeFactory scopeFactory) : BackgroundService
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await mqttService.Connect();
        await mqttService.Subscribe("measurement/+", OnMessageReceived);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!mqttService.ClientIsConnected)
        {
            await mqttService.Reconnect();
        }
    }

    private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        Measurement? telemetry = JsonSerializer.Deserialize<Measurement>(Encoding.Default.GetString(eventArgs.ApplicationMessage.PayloadSegment));

        if (telemetry != null)
        {
            AsyncServiceScope scope = scopeFactory.CreateAsyncScope();
            IMeasurementRepository measurementRepository = scope.ServiceProvider.GetRequiredService<IMeasurementRepository>();

            await measurementRepository.AddMeasurement(telemetry);
        }

    }
}