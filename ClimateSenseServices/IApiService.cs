using ClimateSenseModels;
using IdentityModel.OidcClient;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;

namespace ClimateSenseServices;

public interface IApiService
{
   Task<List<string>> GetLocations();
   Task<List<ClimateMeasurement>> GetMeasurements(string location, DateTime? from, MeasurementType type);
}