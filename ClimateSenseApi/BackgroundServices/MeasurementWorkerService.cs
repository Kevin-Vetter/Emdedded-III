using System.ComponentModel;
using System.Text;
using System.Text.Json;
using ClimateSenseApi.Models;
using ClimateSenseApi.Services;
using MQTTnet.Client;

namespace ClimateSenseApi.BackgroundServices;

public class MeasurementWorkerService(ILogger<MeasurementWorkerService> logger, IMqttService mqttService, IInfluxDbService influxDbService) : BackgroundService
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
        ClimateMeasurement? telemetry = JsonSerializer.Deserialize<ClimateMeasurement>(Encoding.Default.GetString(eventArgs.ApplicationMessage.PayloadSegment));

        if (telemetry != null)
            await influxDbService.Write(telemetry);

    }
}