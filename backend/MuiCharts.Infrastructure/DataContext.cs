using Microsoft.EntityFrameworkCore;
using MuiCharts.Domain.Models;
using MuiCharts.Infrastructure.Configurations;

namespace MuiCharts.Infrastructure;

/// <summary>
/// Represents the database context for MuiCharts application.
/// </summary>
public class DataContext : DbContext
{
	/// <summary>
	/// <see cref="Point"/> table.
	/// </summary>
	public DbSet<Point> Points { get; set; }

	/// <summary>
	/// <see cref="Track"/> table.
	/// </summary>
	public DbSet<Track> Tracks { get; set; }

	/// <inheritdoc/>
	public DataContext() : base() {}

	/// <inheritdoc/>
	public DataContext(DbContextOptions<DataContext> options) : base(options) {}

	/// <inheritdoc/>
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder
			.UseSqlite("Data Source=data.db")
			.EnableSensitiveDataLogging(
				Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
			);
	}

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new PointEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new TrackEntityTypeConfiguration());
	}
}
