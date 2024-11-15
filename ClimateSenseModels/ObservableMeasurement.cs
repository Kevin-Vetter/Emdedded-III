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
    [NotifyPropertyChangedFor(nameof(ValueWithDenomination))]
    private MeasurementType? measurementType;

    [ObservableProperty]
    private string? location;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ValueWithDenomination))]
    private double value;

    public string ValueWithDenomination => MeasurementType.HasValue ? Value + DenominationDictionary.Denominations[MeasurementType.Value] : "";
}