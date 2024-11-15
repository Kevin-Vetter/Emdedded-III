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
            var rooms = await _apiService.GetLocations();
            RoomList.Clear();
            foreach (var item in rooms)
            {
                RoomList.Add(item);
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
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
