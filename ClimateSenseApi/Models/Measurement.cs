
namespace ClimateSenseApi.Models;

public class Measurement
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;

    public string Device { get; set; } = "";

    public int MeasurementType { get; set; }

    public string Location { get; set; } = "";

    public double Value { get; set; }
}