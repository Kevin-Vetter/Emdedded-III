using System.Collections.ObjectModel;
using ClimateSenseModels;
using ClimateSenseServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClimateSenseMAUI.ViewModel;

[QueryProperty(nameof(DashboardClimateInput), "item")]
public partial class RoomDetailViewModel : BaseViewModel
{
    // Find out how telemetry data model looks like.  Then work it out from there, becasue atm i have no clue :D
    [ObservableProperty] private DashboardClimateInput _input;

    private readonly IApiService _apiService;
    public RoomDetailViewModel(IApiService service)
    {
        this._apiService = service;
        GetRoom(_input);
    }
    public ObservableCollection<ClimateMeasurement> Measurementlist { get; } = new();


    [RelayCommand]
   private async Task GetRoom(DashboardClimateInput input)
    {
        if (input.from == null)
        {
            input.from = DateTime.Now.AddHours(-1);
        }
        var messurement = await _apiService.GetRoomMessurent(input.roomname, input.from, input.type);
        Measurementlist.Clear();
        foreach (var item in messurement)
        {
            Measurementlist.Add(item);
        }

    }
    
    
}