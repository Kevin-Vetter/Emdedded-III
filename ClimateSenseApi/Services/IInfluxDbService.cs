using ClimateSenseApi.Models;

namespace ClimateSenseApi.Services;

public interface IInfluxDbService
{
    Task Write(ClimateMeasurement measurement);
    List<ClimateMeasurement> GetMeasurements(string? location, DateTime? from, int? measurementType);
    List<string> GetLocations();
}