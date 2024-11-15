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
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RoomDetailPage), typeof(RoomDetailPage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(NotificationPage), typeof(NotificationPage));
            BindingContext = IPlatformApplication.Current?.Services.GetRequiredService<NotificationViewModel>();
        }
    }
}
