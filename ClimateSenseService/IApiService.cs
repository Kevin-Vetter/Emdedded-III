using ClimateSenseModels;

namespace ClimateSenseService;

public interface IApiService
{
    Task<List<ClimateMeasurement>> RefreshDataAsync();
    Task SaveClimateMeasurementAsync(ClimateMeasurement item, bool isNewItem = false);
    Task DeleteClimateMeasurementAsync(int id);
}