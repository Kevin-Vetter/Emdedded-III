namespace ClimateSenseModels;

public class DashboardRoom(string location)
{
    public string Location { get; set; } = location;
    public List<Measurement> Measurements { get; set; } = new();

    public void UpdateMeasurements(Measurement measurement)
    {
        Measurement? existingMeasurement = Measurements.FirstOrDefault(x => x.MeasurementType == measurement.MeasurementType);

        if (existingMeasurement == null)
        {
            Measurements.Add(measurement);
        }
        else
        {
            existingMeasurement.MeasurementType = measurement.MeasurementType;
            existingMeasurement.Value = measurement.Value;
            existingMeasurement.Location = measurement.Location;
            existingMeasurement.Device = measurement.Device;
            existingMeasurement.Timestamp = measurement.Timestamp;
        }
    }
}