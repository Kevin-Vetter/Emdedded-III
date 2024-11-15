using System.Diagnostics;
using System.Text;
using System.Text.Json;
using ClimateSenseModels;

namespace ClimateSenseServices;

public class ApiService : IApiService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    public List<ClimateMeasurement> Items { get; private set; }
    public List<string> Locations { get; private set; }

    public ApiService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<List<ClimateMeasurement>> RefreshDataAsync()
    {
        Items = new List<ClimateMeasurement>();

        UriBuilder builder = new(Constants.BaseUrl) { Path = Constants.Endpoint };
        try
        {
            HttpResponseMessage response = await _client.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<ClimateMeasurement>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<List<string>> GetLocations()
    {
        Locations = new List<string>();
        try
        {
            HttpResponseMessage response = null;
            UriBuilder builder = new(Constants.BaseUrl) { Path = $"{Constants.Endpoint}/locations" };
            response = await _client.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Locations = JsonSerializer.Deserialize<List<string>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Locations;
    }

    public async Task<List<ClimateMeasurement>> GetRoomMessurent(string room, DateTime? from, MeasurementType type)
    {
        Items = new List<ClimateMeasurement>();
        try
        {
            HttpResponseMessage response = null;
            UriBuilder builder = new(Constants.BaseUrl) { Path = Constants.Endpoint + $"{"?location="+room+"&from="+from+"&measurementType="+(int)type}" };
            response = await _client.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<ClimateMeasurement>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return Items;
    }
    
}