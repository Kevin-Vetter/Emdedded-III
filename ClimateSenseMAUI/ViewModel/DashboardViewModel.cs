using ClimateSenseMAUI.View;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClimateSenseModels;
using ClimateSenseServices;
using CommunityToolkit.Mvvm.Messaging;
using MQTTnet.Client;

namespace ClimateSenseMAUI.ViewModel
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public DashboardViewModel(IApiService service)
        {
            _apiService = service;
        }

        public ObservableCollection<DashboardRooms> RoomList { get; } = new();

        [RelayCommand]
        async Task GetRooms()
        {
            var rooms = await _apiService.GetLocations();
            RoomList.Clear();
            foreach (var item in rooms)
            {
                MeasurementType[] measurementTypes = Enum.GetValues<MeasurementType>();

                DashboardRooms room = new DashboardRooms()
                {
                    RoomName = item,
                    CurrentMeasurements = new()
                };

                for (int i = 0; i < measurementTypes.Length; i++)
                {
                    Measurement measurement = new Measurement();
                    room.CurrentMeasurements.Add(measurement);

                    WeakReferenceMessenger.Default.Register<Measurement, string>(measurement,$"{item}/{measurementTypes[i]}", (recipient, message) =>
                    {
                        recipient = message;
                    });
                }

                RoomList.Add(room);
            }
        }

        [RelayCommand]
        async Task GoToRoomDetails(DashboardRooms item)
        {
           var room = new DashboardRooms
            {
                RoomName = item.RoomName,
            };
            await Shell.Current.GoToAsync(nameof(RoomDetailPage),new Dictionary<string, object>
            {
                {"item",item}
            });
        }
        [RelayCommand]
        public async Task LogOut()
        {
            await SecureStorage.SetAsync("access_token", "");
            await Shell.Current.GoToAsync("..");
        }
    }
}
