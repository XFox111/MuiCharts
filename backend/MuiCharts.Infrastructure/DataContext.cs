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

	/// <summary>
	/// Initializes a new instance of <see cref="DataContext"/>.
	/// </summary>
	/// <param name="options">The options for this context.</param>
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
		Database.Migrate();
	}

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new PointEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new TrackEntityTypeConfiguration());
	}
}
