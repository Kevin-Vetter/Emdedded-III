using System.Collections.ObjectModel;
using ClimateSenseModels;
using ClimateSenseServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClimateSenseMAUI.ViewModel;

[QueryProperty(nameof(DashboardRooms), "item")]
public partial class RoomDetailViewModel : BaseViewModel
{

    // dependency injection
    private readonly IApiService _apiService;
    [ObservableProperty] public DashboardRooms _roomName;
    private DashboardClimateInput _dashboardClimateInput { get; set; }
    public RoomDetailViewModel(IApiService service)
    {
        _apiService = service;
        _dashboardClimateInput = new DashboardClimateInput
        {
            From = DateTime.UtcNow.AddHours(-1),
            Roomname = _roomName.RoomName,
            Type = MeasurementType.Temperature
        };
      GetRoom(_dashboardClimateInput);
    }
    public ObservableCollection<ClimateMeasurement> Measurementlist { get; } = new();

    [RelayCommand]
   private async Task GetRoom(DashboardClimateInput input)
    {
        var messurement = await _apiService.GetRoomMessurent(input.Roomname, input.From, input.Type);
        Measurementlist.Clear();
        foreach (var item in messurement)
        {
            Measurementlist.Add(item);
        }

    }
    
    
}