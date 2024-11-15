namespace ClimateSenseModels;

public class DashboardClimateInput
{
    public string Roomname { get; set; }
    public MeasurementType Type { get; set; }
    public DateTime From { get; set; }
}