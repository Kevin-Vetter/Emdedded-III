using System.Diagnostics;
using System.Text;
using System.Text.Json;
using ClimateSenseModels;
using Auth0.OidcClient;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;



namespace ClimateSenseServices;

public class ApiService : IApiService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    public List<ClimateMeasurement> Items { get; private set; }
    public List<string> Locations { get; private set; }
    private readonly Auth0Client _auth0Client;

    public ApiService(Auth0Client auth0Client)
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        _auth0Client = auth0Client;
    }

    public async Task<List<ClimateMeasurement>> RefreshDataAsync(JsonWebToken token)
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

    public async Task<List<string>> GetLocations(JsonWebToken token)
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

    public async Task<List<ClimateMeasurement>> GetRoomMessurent(JsonWebToken token, string room, DateTime? from, MeasurementType type)
    {
        Items = new List<ClimateMeasurement>();
        try
        {
            _client.DefaultRequestHeaders.Authorization
                = new BasicAuthenticationHeaderValue("Bearer", token.ToString());
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