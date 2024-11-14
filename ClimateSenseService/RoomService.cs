using ClimateSenseModels;

namespace ClimateSenseService;

public class RoomService(IApiService apiService)
{
    public Task<List<ClimateMeasurement>> GetClimateMeasurements() => apiService.RefreshDataAsync();
    public Task SaveRoomSettingAsync(ClimateMeasurement item, bool isNewItem = false)
    {
        //
        // return restRepo.SaveTodoItemAsync(item, isNewItem);
        return null;
    }
}