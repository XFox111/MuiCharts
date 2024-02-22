using Microsoft.Extensions.DependencyInjection;
using MuiCharts.Domain.Repositories;
using MuiCharts.Infrastructure.Repositories;

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
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddDbContext<DataContext>();
		services.AddScoped<IPointRepository, PointRepository>();
		services.AddScoped<ITrackRepository, TrackRepository>();

		return services;
	}
}
