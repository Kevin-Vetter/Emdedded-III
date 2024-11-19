using ClimateSenseModels;
using IdentityModel.OidcClient;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ClimateSenseServices;

public interface IApiService
{
   Task<LoginResult> Login();
   Task<List<ClimateMeasurement>> RefreshDataAsync(JsonWebToken token);
   Task<List<ClimateMeasurement>> GetRoomMessurent(JsonWebToken token, string room, DateTime? from, MeasurementType type);
   Task<List<string>> GetLocations(JsonWebToken token);
}