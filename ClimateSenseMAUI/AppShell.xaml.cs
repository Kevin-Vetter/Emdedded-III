using ClimateSenseMAUI.View;

namespace ClimateSenseMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RoomDetailPage), typeof(RoomDetailPage));
        }
    }
}
