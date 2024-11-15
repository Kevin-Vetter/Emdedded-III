using CommunityToolkit.Mvvm.ComponentModel;

namespace ClimateSenseModels;

public partial class ObservableMeasurement : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private DateTime timestamp;

    [ObservableProperty]
    private string? device;

    [ObservableProperty]
    private MeasurementType? measurementType;

    [ObservableProperty]
    private string? location;

    [ObservableProperty]
    private double value;
}