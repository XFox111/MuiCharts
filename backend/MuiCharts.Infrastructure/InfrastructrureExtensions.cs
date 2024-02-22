using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MuiCharts.Domain.Repositories;
using MuiCharts.Infrastructure.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace MuiCharts.Infrastructure;

/// <summary>
/// Provides extension methods for configuring infrastructure services.
/// </summary>
public static class InfrastructrureExtensions
{
	/// <summary>
	/// Adds infrastructure services to the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
	/// <returns>The modified <see cref="IServiceCollection"/>.</returns>
	public static void AddInfrastructure(this IHostApplicationBuilder builder)
	{
		builder.Services.AddDbContext<DataContext>(options =>
		{
			options
				.UseSqlite(builder.Configuration.GetConnectionString(nameof(DataContext)))
				.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
		});

		builder.Services.AddScoped<IPointRepository, PointRepository>();
		builder.Services.AddScoped<ITrackRepository, TrackRepository>();
	}
}
