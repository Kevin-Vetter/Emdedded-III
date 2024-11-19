namespace ClimateSenseModels;

public class Measurement
{
    public DateTime Timestamp { get; set; }

    public string? Device { get; set; }

    public MeasurementType MeasurementType { get; set; }

    public string Location { get; set; } = null!;

    public double Value { get; set; }
}