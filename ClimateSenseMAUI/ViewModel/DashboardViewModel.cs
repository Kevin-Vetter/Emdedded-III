using ClimateSenseMAUI.View;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateSenseModels;
using ClimateSenseServices;

namespace ClimateSenseMAUI.ViewModel
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public DashboardViewModel(IApiService service)
        {
            this._apiService = service;
         
            
        }
        public ObservableCollection<DashboardRooms> RoomList { get; } = new();
        
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
            await Shell.Current.GoToAsync("..");
        }
    }
}
