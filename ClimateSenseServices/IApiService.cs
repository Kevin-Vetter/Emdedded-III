using ClimateSenseModels;

namespace ClimateSenseServices;

public interface IApiService
{
   Task<List<string>> GetLocations();
   Task<List<ClimateMeasurement>> GetMeasurements(string location, DateTime? from, MeasurementType type);
   Task PostWindowAngle(int windowAngle);
}