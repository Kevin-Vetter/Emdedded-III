using ClimateSenseApi.Models;
using ClimateSenseApi.Repositories;
using ClimateSenseApi.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<List<Measurement>> GetMeasurements(string? location, DateTime? from, int? measurementType)
    {
        return await measurementRepository.GetMeasurements(location, from, measurementType);
    }
}