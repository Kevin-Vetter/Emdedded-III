using ClimateSenseApi.Models;
using ClimateSenseApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClimateSenseApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TelemetryMeasurementController(IInfluxDbService influxDbService) : ControllerBase
{
    [HttpGet]
    public List<TelemetryMeasurement> GetList()
    {
        return influxDbService.Read();
    }
}