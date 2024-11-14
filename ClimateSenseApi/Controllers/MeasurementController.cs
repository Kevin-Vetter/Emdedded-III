using ClimateSenseApi.Repositories;
using ClimateSenseModels;
using Microsoft.AspNetCore.Mvc;
using Measurement = ClimateSenseApi.Entities.Measurement;

namespace ClimateSenseApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController(IMeasurementRepository measurementRepository) : ControllerBase
{
    [HttpGet("locations")]
    public async Task<List<string>> GetList()
    {
        return await measurementRepository.GetLocations();
    }

    [HttpGet("")]
    public async Task<List<Measurement>> GetMeasurements(string? location, DateTime? from, MeasurementType? measurementType)
    {
        return await measurementRepository.GetMeasurements(location, from, measurementType);
    }
}