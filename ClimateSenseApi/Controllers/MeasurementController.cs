using ClimateSenseApi.Repositories;
using ClimateSenseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Measurement = ClimateSenseApi.Entities.Measurement;

namespace ClimateSenseApi.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class MeasurementController(IMeasurementRepository measurementRepository) : ControllerBase
{
    [HttpGet("locations")]
    [Authorize(Policy = Permissions.SensorRead)]
    public async Task<List<string>> GetList() => await measurementRepository.GetLocations();
    

    [HttpGet("")]
    [Authorize(Permissions.SensorRead)]
    public async Task<List<Measurement>> GetMeasurements(string? location, DateTime? from, MeasurementType? measurementType)
    {
        return await measurementRepository.GetMeasurements(location, from, measurementType);
    }
}