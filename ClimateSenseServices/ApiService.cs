using System.Text.Json;
using ClimateSenseModels;


namespace ClimateSenseServices;

public class ApiService(HttpClient httpClient) : IApiService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task<List<string>> GetLocations()
    {
        UriBuilder builder = new() { Path = "Measurement/locations" };
        HttpResponseMessage response = await httpClient.GetAsync(builder.Path);

        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<string>>(content, _serializerOptions) ?? new List<string>();
    }

    public async Task<List<ClimateMeasurement>> GetMeasurements(string location, DateTime? from, MeasurementType type)
    {
        UriBuilder uriBuilder = new()
        {
            Path = "Measurement",
            Query = $"location={location}&from={Uri.EscapeDataString(from?.ToString("o") ?? "")}&measurementType={(int)type}"
        };

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.Path + uriBuilder.Query);

        httpResponseMessage.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<List<ClimateMeasurement>>(await httpResponseMessage.Content.ReadAsStringAsync(), _serializerOptions) ?? new List<ClimateMeasurement>();
    }

    public async Task PostWindowAngle(int windowAngle)
    {
        UriBuilder uriBuilder = new UriBuilder()
        {
            Path = "Action/servo",
            Query = $"turnDegrees={windowAngle}"
        };

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.Path + uriBuilder.Query, null);
        httpResponseMessage.EnsureSuccessStatusCode();
    }
}