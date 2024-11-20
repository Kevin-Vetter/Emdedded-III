using MQTTnet.Client;
using MQTTnet.Protocol;

namespace ClimateSenseServices;

public interface IMqttService
{
    Task<MqttClientConnectResult> ConnectAsync(MqttClientOptionsBuilder? optionsBuilder = null, CancellationToken? cancellationToken = null);
    Task ReconnectAsync(CancellationToken? cancellationToken = null);
    Task<MqttClientPublishResult> PublishAsync(string topic, dynamic payload, bool asJson = false, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce, CancellationToken? cancellationToken = null);
    Task SubscribeAsync(string topic, Func<MqttApplicationMessageReceivedEventArgs, Task> onMessageReceivedEvent, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtLeastOnce, CancellationToken? cancellationToken = null);
    MqttClientOptionsBuilder GetOptionsBuilder();
    IMqttClient GetClient();
    bool ClientIsConnected { get; }
}