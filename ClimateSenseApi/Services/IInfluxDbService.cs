using ClimateSenseApi.Models;

namespace ClimateSenseApi.Services;

public interface IInfluxDbService
{
    Task Write(Telemetry telemetry);
    List<TelemetryMeasurement> Read();
}