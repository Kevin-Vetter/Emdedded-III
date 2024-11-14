using System.Collections.ObjectModel;
using ClimateSenseModels;
using ClimateSenseService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClimateSenseMAUI.ViewModel;

[QueryProperty(nameof(_input), "item")]
public partial class RoomDetailViewModel : BaseViewModel
{

    private readonly IApiService _apiService;
    public RoomDetailViewModel(IApiService service)
    {
        this._apiService = service;
        GetRoom(_input.roomname, null, _input.type);
    }
    // Find out how telemetry data model looks like.  Then work it out from there, becasue atm i have no clue :D
    [ObservableProperty] private DashboardClimateInput _input;
    public ObservableCollection<ClimateMeasurement> Measurementlist { get; } = new();


    [RelayCommand]
     async Task GetRoom(string room, DateTime? from, MeasurementType type)
    {
        if (from == null)
        {
            from = DateTime.Now.AddHours(-1);
        }
        var messurement = await _apiService.GetRoomMessurent(room, from, type);
        Measurementlist.Clear();
        foreach (var item in messurement)
        {
            Measurementlist.Add(item);
        }

    }
    
    
}