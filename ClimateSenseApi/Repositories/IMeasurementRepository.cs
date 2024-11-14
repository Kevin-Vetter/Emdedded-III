using ClimateSenseModels;
using Measurement = ClimateSenseApi.Entities.Measurement;

namespace ClimateSenseApi.Repositories;

public interface IMeasurementRepository
{
    Task AddMeasurement(Measurement measurement);
    Task<List<Measurement>> GetMeasurements(string? location, DateTime? from, MeasurementType? measurementType);
    Task<List<string>> GetLocations();
}