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

        public async Task StartSubscribingToMqtt()
        {
            await mqttService.Connect();
            await mqttService.Subscribe("location/+/measurement/+", MessageReceivedHandler);
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
