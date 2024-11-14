using ClimateSenseApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClimateSenseApi.Contexts;

public class MeasurementContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Measurement> Measurements { get; set; }
}