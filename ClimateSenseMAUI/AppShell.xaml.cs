using ClimateSenseMAUI.View;

using System.Text.Json;
using ClimateSenseMAUI.ViewModel;
using ClimateSenseModels;
using ClimateSenseServices;
using CommunityToolkit.Mvvm.Messaging;
using MQTTnet.Client;

namespace ClimateSenseMAUI
{
    public partial class AppShell : Shell
    {
        private readonly IMqttService _mqttService;

        public AppShell()
        {
            _mqttService = IPlatformApplication.Current!.Services.GetRequiredService<IMqttService>();
            InitializeComponent();
            Routing.RegisterRoute(nameof(RoomDetailPage), typeof(RoomDetailPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            BindingContext = IPlatformApplication.Current?.Services.GetRequiredService<NotificationViewModel>();
        }

        protected override async void OnAppearing()
        {
            await _mqttService.Connect();
            await _mqttService.Subscribe("location/+/measurement/+", MessageReceivedHandler);
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
