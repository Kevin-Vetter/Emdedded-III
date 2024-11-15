using System.Collections.ObjectModel;

namespace ClimateSenseModels;

public class DashboardRooms
{
    public string RoomName { get; set; }
    public ObservableCollection<ObservableMeasurement> CurrentMeasurements { get; set; }
}