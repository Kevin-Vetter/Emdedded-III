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
using System.IdentityModel.Tokens.Jwt;

namespace ClimateSenseMAUI.ViewModel
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<DashboardRooms> RoomList { get; } = new();

        public DashboardViewModel(IApiService service)
        {
            _apiService = service;
        }

        public async Task CheckLoggedInAsync()
        {
            var token = await SecureStorage.GetAsync("access_token");
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            }
        }


        [RelayCommand]
        async Task GetRooms()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsRefreshing = true;
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

                    RegisterMessengers(measurementTypes, room, item);

                    RoomList.Add(room);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        private void RegisterMessengers(MeasurementType[] measurementTypes, DashboardRooms room, string item)
        {
            for (int i = 0; i < measurementTypes.Length; i++)
            {
                ObservableMeasurement measurement = new ObservableMeasurement();
                room.CurrentMeasurements.Add(measurement);

                if (WeakReferenceMessenger.Default.IsRegistered<Measurement, string>(this, $"{item}/{measurementTypes[i]}"))
                {
                    WeakReferenceMessenger.Default.Unregister<Measurement, string>(this, $"{item}/{measurementTypes[i]}");
                }

                WeakReferenceMessenger.Default.Register<Measurement, string>(this, $"{item}/{measurementTypes[i]}", (recipient, message) =>
                {
                    measurement.MeasurementType = message.MeasurementType;
                    measurement.Timestamp = message.Timestamp;
                    measurement.Location = message.Location;
                    measurement.Value = message.Value;
                    measurement.Device = message.Device;
                });
            }
        }

        [RelayCommand]
        async Task GoToRoomDetails(DashboardRooms item)
        {
            // Package package=  new Package() { packageId = lbl.Text, packageName = "Pack" };  
            
            // await Shell.Current.GoToAsync(nameof(RoomDetailPage)+$"?item={item}");
            await Shell.Current.GoToAsync(nameof(RoomDetailPage), new Dictionary<string, object>
            {
                {"item",item}
            });
        }

        [RelayCommand]
        public async Task LogOut()
        {
            await SecureStorage.SetAsync("access_token", "");
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        public void UnregisterMessengers()
        {
            foreach (DashboardRooms rooms in RoomList)
            {
                foreach (ObservableMeasurement measurement in rooms.CurrentMeasurements)
                {
                    WeakReferenceMessenger.Default.Unregister<Measurement, string>(this, $"{rooms.RoomName}/{measurement.MeasurementType}");
                }
            }
        }
    }
}