namespace ClimateSenseModels;

public class Measurement
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string? Device { get; set; }

    public MeasurementType MeasurementType { get; set; }

    public string? Location { get; set; }

    public double Value { get; set; }
}