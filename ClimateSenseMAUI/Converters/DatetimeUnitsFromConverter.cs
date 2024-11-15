using System.Globalization;

namespace ClimateSenseMAUI.Converters;

public class DatetimeUnitsFromConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DateTime datetime)
            return null;

        TimeSpan time = DateTime.Now.Subtract(datetime);

        return time.TotalSeconds switch
        {
            <= 10 => "Now",
            <= 60 => time.Seconds + "s",
            _ => time.TotalMinutes switch
            {
                <= 60 => time.Minutes + "m",
                _ => time.Hours + "h"
            }
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}