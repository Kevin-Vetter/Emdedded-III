using ClimateSenseMAUI.View;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
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
    public partial class DashboardViewModel(IMqttService mqttService) : BaseViewModel
    {
        [RelayCommand]
        public async Task LogOut()
        {
            await SecureStorage.SetAsync("access_token", "");
            await Shell.Current.GoToAsync("..");
        }
    }
}
