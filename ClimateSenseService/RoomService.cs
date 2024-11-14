using ClimateSenseModels;

namespace ClimateSenseService;

public class RoomService(IApiService apiService)
{
    public Task<List<ClimateMeasurement>> GetClimateMeasurements() => apiService.RefreshDataAsync();
}