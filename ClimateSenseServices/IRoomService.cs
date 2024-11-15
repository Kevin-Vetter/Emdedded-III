using ClimateSenseModels;

namespace ClimateSenseServices;

public interface IRoomService
{
    Task<List<ClimateMeasurement>> GetClimateMeasurements();
    Task SaveRoomSettingAsync(ClimateMeasurement item, bool isNewItem = false);
}