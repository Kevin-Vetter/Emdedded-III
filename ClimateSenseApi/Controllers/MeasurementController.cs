using ClimateSenseApi.Models;
using ClimateSenseApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClimateSenseApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController(IInfluxDbService influxDbService) : ControllerBase
{
    [HttpGet("locations")]
    public List<string> GetList()
    {
        return influxDbService.GetLocations();
    }

    [HttpGet("")]
    public List<ClimateMeasurement> GetMeasurements(string? location, DateTime? from, int? measurementType)
    {
        return influxDbService.GetMeasurements(location, from, measurementType);
    }
}