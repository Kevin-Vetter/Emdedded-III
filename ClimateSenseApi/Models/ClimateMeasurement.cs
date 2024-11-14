using InfluxDB.Client.Core;

namespace ClimateSenseApi.Models;

[Measurement(nameof(ClimateMeasurement))]
public class ClimateMeasurement
{
    [Column(IsTimestamp = true)]
    public DateTime Timestamp { get; set; } = DateTime.Now;

    [Column("device", IsTag = true)]
    public string Device { get; set; } = "";

    [Column("measurementType", IsTag = true)]
    public int MeasurementType { get; set; }

    [Column("location", IsTag = true)]
    public string Location { get; set; } = "";

    [Column("value")]
    public double Value { get; set; }
}