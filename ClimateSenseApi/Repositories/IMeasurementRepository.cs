using ClimateSenseApi.Models;

namespace ClimateSenseApi.Repositories;

public interface IMeasurementRepository
{
    Task AddMeasurement(Measurement measurement);
    Task<List<Measurement>> GetMeasurements(string? location, DateTime? from, int? measurementType);
    Task<List<string>> GetLocations();
}