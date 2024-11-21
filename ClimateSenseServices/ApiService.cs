using System.Diagnostics;
using System.Text;
using System.Text.Json;
using ClimateSenseModels;
using Auth0.OidcClient;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using IdentityModel.OidcClient;
using System.Threading.RateLimiting;



namespace ClimateSenseServices;

public class ApiService : IApiService
{
    private readonly HttpClient _client = new();

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task<List<string>> GetLocations(string token)
    {

        UriBuilder builder = new(Constants.BaseUrl) { Path = "Measurement/locations" };
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("authorization", token);
        HttpResponseMessage response = await _client.GetAsync(builder.Uri);

        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<string>>(content, _serializerOptions) ?? new List<string>();
    }

    public async Task<List<ClimateMeasurement>> GetMeasurements(string location, DateTime? from, MeasurementType type)
    {
        string baseUrl = Constants.BaseUrl;

        UriBuilder uriBuilder = new UriBuilder(new Uri(baseUrl))
        {
            Path = "Measurement"
        };

        uriBuilder.Query = $"location={location}&from={from?.ToString()}&measurementType={(int)type}";

        HttpResponseMessage httpResponseMessage = await _client.GetAsync(uriBuilder.Uri);

        httpResponseMessage.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<List<ClimateMeasurement>>(await httpResponseMessage.Content.ReadAsStringAsync(), _serializerOptions) ?? new List<ClimateMeasurement>();
    }
}