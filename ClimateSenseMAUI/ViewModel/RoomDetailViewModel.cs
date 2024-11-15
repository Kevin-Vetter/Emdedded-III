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
      // GetRoom();
    }
    public ObservableCollection<ClimateMeasurement> Measurementlist { get; } = new();

    [RelayCommand]
   private async Task GetRoom()
   {
       var messurement = await _apiService.RefreshDataAsync();
        Measurementlist.Clear();
        foreach (var item in messurement)
        {
            Measurementlist.Add(item);
        }

    }
    [RelayCommand]
      private async Task GetRoomWeek()
        {
            var messurement = await _apiService.GetRoomMessurent(_roomName.RoomName, DateTime.UtcNow.AddDays(-7), MeasurementType.Humidity);
            Measurementlist.Clear();
            foreach (var item in messurement)
            {
                Measurementlist.Add(item);
            }
    
        }
      
    [RelayCommand]
        private async Task GetRoomDay()
        {
          
            var messurement = await _apiService.GetRoomMessurent(_roomName.RoomName, DateTime.UtcNow.AddHours(-24), MeasurementType.Humidity);
            Measurementlist.Clear();
            foreach (var item in messurement)
            {
                Measurementlist.Add(item);
            }

        }

    
    
}