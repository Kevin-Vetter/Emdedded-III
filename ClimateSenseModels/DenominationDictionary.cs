namespace ClimateSenseModels
{
    public static class DenominationDictionary
    {
        public static Dictionary<MeasurementType, string> Denominations = new()
        {
            {MeasurementType.Humidity, "%" },
            {MeasurementType.Temperature, "°C" },

        };
    }
}
