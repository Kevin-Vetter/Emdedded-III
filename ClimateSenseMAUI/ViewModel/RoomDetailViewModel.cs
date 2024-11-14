using System.Collections.ObjectModel;
using ClimateSenseModels;
using ClimateSenseService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClimateSenseMAUI.ViewModel;

[QueryProperty(nameof(ClimateMeasurement), "room")]
public partial class RoomDetailViewModel : BaseViewModel
{

    private readonly IApiService _apiService;
    public RoomDetailViewModel(IApiService service)
    {
        this._apiService = service;
    }
    // Find out how telemetry data model looks like.  Then work it out from there, becasue atm i have no clue :D

    [ObservableProperty] ClimateMeasurement currentMeasurement;


    [RelayCommand]
    async Task GetRoomCommand()
    {
        
    }
    
    
}