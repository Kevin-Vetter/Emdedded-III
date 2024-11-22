using System.Text;
using ClimateSenseModels;
using ClimateSenseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet.Protocol;

namespace ClimateSenseApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ActionController(ILogger<ActionController> logger, IMqttService mqttService) : ControllerBase
{
    private const string _topic = "action";

    [HttpPost("led-on")]
    public async Task<bool> LedOn()
    {
        MqttClientPublishResult result = await mqttService.PublishAsync($"{_topic}/led", "on");
        return result.IsSuccess;
    }

    [HttpPost("led-off")]
    public async Task<bool> LedOff()
    {
        MqttClientPublishResult result = await mqttService.PublishAsync($"{_topic}/led", "off");
        return result.IsSuccess;
    }

    [HttpPost("servo")]
    [Authorize(Permissions.ServoWrite)]
    public async Task<bool> Servo(int turnDegrees)
    {
        MqttClientPublishResult result = await mqttService.PublishAsync($"{_topic}/servo", turnDegrees);
        return result.IsSuccess;
    }
}