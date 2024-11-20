using System.Text;
using System.Text.Json;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace ClimateSenseServices;

public class MqttService(MqttFactory mqttFactory, IMqttClient mqttClient, MqttClientOptionsBuilder builder) : IMqttService, IDisposable
{
    public bool ClientIsConnected => mqttClient.IsConnected;

    public async Task<MqttClientConnectResult> ConnectAsync(MqttClientOptionsBuilder? optionsBuilder = null, CancellationToken? cancellationToken = null)
    {
        if (ClientIsConnected)
        {
            await mqttClient.DisconnectAsync();
        }

        optionsBuilder ??= builder;
        return await mqttClient.ConnectAsync(optionsBuilder.Build(), cancellationToken ?? CancellationToken.None);
    }

    public async Task ReconnectAsync(CancellationToken? cancellationToken = null)
    {
        await mqttClient.ReconnectAsync(cancellationToken ?? CancellationToken.None);
    }

    public async Task<MqttClientPublishResult> PublishAsync(string topic, dynamic payload, bool asJson = false, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce, CancellationToken? cancellationToken = null)
    {
        if (!ClientIsConnected)
        {
            await ReconnectAsync();
        }

        return await mqttClient.PublishAsync(new MqttApplicationMessage()
        {
            Topic = topic,
            PayloadSegment = Encoding.Default.GetBytes(asJson ? JsonSerializer.Serialize(payload) : payload.ToString()),
            QualityOfServiceLevel = qos
        }, cancellationToken ?? CancellationToken.None);
    }

    public async Task SubscribeAsync(string topic, Func<MqttApplicationMessageReceivedEventArgs, Task> onMessageReceivedEvent, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtLeastOnce, CancellationToken? cancellationToken = null)
    {
        MqttClientSubscribeOptions mqttClientSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(topic, qos)
            .Build();

        mqttClient.ApplicationMessageReceivedAsync += onMessageReceivedEvent;

        if (!ClientIsConnected)
        {
            await ReconnectAsync();
        }

        await mqttClient.SubscribeAsync(mqttClientSubscribeOptions, cancellationToken ?? CancellationToken.None);
    }

    public MqttClientOptionsBuilder GetOptionsBuilder()
    {
        return builder;
    }

    public IMqttClient GetClient()
    {
        return mqttClient;
    }

    public void Dispose()
    {
        mqttClient.Dispose();
    }
}