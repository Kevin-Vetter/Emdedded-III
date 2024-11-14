using ClimateSenseModels;

namespace ClimateSenseService;

public interface IRoomService
{
    Task<List<ClimateMeasurement>> GetClimateMeasurements();
    Task SaveRoomSettingAsync(ClimateMeasurement item, bool isNewItem = false);
}