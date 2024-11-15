using System.Collections.ObjectModel;

namespace ClimateSenseModels;

public class DashboardRooms
{
    public string RoomName { get; set; }
    public ObservableCollection<Measurement> CurrentMeasurements { get; set; }
}