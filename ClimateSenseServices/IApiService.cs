using ClimateSenseModels;

namespace ClimateSenseServices;

public interface IApiService
{
   Task<List<ClimateMeasurement>> RefreshDataAsync();
   Task<List<ClimateMeasurement>> GetRoomMessurent(string room, DateTime? from, MeasurementType type);
   Task GetLocations();
}