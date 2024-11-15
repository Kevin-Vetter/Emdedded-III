using ClimateSenseModels;

namespace ClimateSenseServices;

public class RoomService(IApiService apiService) : IRoomService
{
    public Task<List<ClimateMeasurement>> GetClimateMeasurements() => apiService.RefreshDataAsync();
    public Task SaveRoomSettingAsync(ClimateMeasurement item, bool isNewItem = false)
    {
        //
        // return restRepo.SaveTodoItemAsync(item, isNewItem);
        return null;
    }
}