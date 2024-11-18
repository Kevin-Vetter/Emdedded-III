using ClimateSenseModels;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ClimateSenseServices;

public interface IApiService
{
   Task<List<ClimateMeasurement>> RefreshDataAsync(JsonWebToken token);
   Task<List<ClimateMeasurement>> GetRoomMessurent(JsonWebToken token, string room, DateTime? from, MeasurementType type);
   Task<List<string>> GetLocations(JsonWebToken token);
}