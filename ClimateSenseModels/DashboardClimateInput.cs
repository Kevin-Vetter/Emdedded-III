namespace ClimateSenseModels;

public class DashboardClimateInput
{
    public string roomname { get; set; }
    public MeasurementType type { get; set; }
    public DateTime from { get; set; }
}