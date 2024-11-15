using CommunityToolkit.Mvvm.Input;
using Auth0.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateSenseMAUI.View;
using IdentityModel.OidcClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using ClimateSenseModels;
using ClimateSenseServices;
using CommunityToolkit.Mvvm.Messaging;
using MQTTnet.Client;

namespace ClimateSenseMAUI.ViewModel
{
    public partial class LoginViewModel(Auth0Client auth0Client, IMqttService mqttService) : BaseViewModel
    {
        public async Task CheckLoggedIn()
        {
          var token = await SecureStorage.GetAsync("access_token");
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                await StartMqttSubscription();
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}", true);
            }
        }

        [RelayCommand]
        public async Task LoginAsync()
        {

            var loginResult = await auth0Client.LoginAsync();

            if (!loginResult!.IsError)
            {
                await StartMqttSubscription();
                await SecureStorage.SetAsync("access_token", loginResult.AccessToken);
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}", true);
            }
            else
                await Shell.Current.DisplayAlert("Login Error!", $"Failed to authenticate.\n{loginResult.ErrorDescription}", "OK");
        }

        private async Task StartMqttSubscription()
        {
            if (!mqttService.ClientIsConnected)
            {
                await mqttService.Connect();
                await mqttService.Subscribe("location/+/measurement/+", MessageReceivedHandler);
            }
        }

        private Task MessageReceivedHandler(MqttApplicationMessageReceivedEventArgs args)
        {
            Measurement? measurement = JsonSerializer.Deserialize<Measurement>(args.ApplicationMessage.PayloadSegment);
            if (measurement == null) return Task.CompletedTask;

            WeakReferenceMessenger.Default.Send(measurement, $"{measurement.Location}/{measurement.MeasurementType}");

            if (measurement.Value > 30)
            {
                WeakReferenceMessenger.Default.Send(new Notification()
                {
                    Message = $"Warning: {Enum.GetName(measurement.MeasurementType)} has reached {measurement.Value}{DenominationDictionary.Denominations[measurement.MeasurementType]}"
                });
            }

            return Task.CompletedTask;
        }
    }
}
