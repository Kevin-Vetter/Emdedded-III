using ClimateSenseApi.Models;
using InfluxDB.Client;
using InfluxDB.Client.Linq;
using Microsoft.Extensions.Options;

namespace ClimateSenseApi.Services;

public class InfluxDbService : IInfluxDbService
{
    private readonly InfluxDBClient _client;
    private readonly InfluxDbInstance _influxDbSettings;

    public InfluxDbService(IOptions<AppSettings> appSettings)
    {
        _influxDbSettings = appSettings.Value.InfluxDbInstance;
        _client = new InfluxDBClient(new InfluxDBClientOptions(_influxDbSettings.Host)
        {
            Bucket = _influxDbSettings.Bucket,
            Token = _influxDbSettings.ApiToken,
            Org = _influxDbSettings.OrganizationId
        });
    }

    public async Task Write(ClimateMeasurement measurement)
    {
        WriteApiAsync writeApi = _client.GetWriteApiAsync();
        await writeApi.WriteMeasurementAsync(measurement);
    }

    public List<ClimateMeasurement> GetMeasurements(string? location, DateTime? from, int? measurementType)
    {
        QueryApiSync queryApi = _client.GetQueryApiSync();

        IQueryable<ClimateMeasurement> query = InfluxDBQueryable<ClimateMeasurement>.Queryable(_influxDbSettings.Bucket, _influxDbSettings.OrganizationId, queryApi).AsQueryable();

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(x => x.Location == location);
        }

        if (from != null)
        {
            query = query.Where(x => x.Timestamp == from);
        }

        if (measurementType != null)
        {
            query = query.Where(x => x.MeasurementType == measurementType);
        }

        return query.ToList();
    }

    public List<string> GetLocations()
    {
        QueryApiSync queryApi = _client.GetQueryApiSync();
        return InfluxDBQueryable<ClimateMeasurement>.Queryable(_influxDbSettings.Bucket, _influxDbSettings.OrganizationId, queryApi).Select(x => x.Location).Distinct().ToList();
    }
}