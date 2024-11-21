using System.Diagnostics;
using System.Text;
using System.Text.Json;
using ClimateSenseModels;
using Auth0.OidcClient;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using IdentityModel.OidcClient;



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
        UriBuilder uriBuilder = new UriBuilder()
        {
            Path = "Measurement",
            Query = $"location={location}&from={from?.ToString()}&measurementType={(int)type}"
        };

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.Path + uriBuilder.Query);

        httpResponseMessage.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<List<ClimateMeasurement>>(await httpResponseMessage.Content.ReadAsStringAsync(), _serializerOptions) ?? new List<ClimateMeasurement>();
    }
}