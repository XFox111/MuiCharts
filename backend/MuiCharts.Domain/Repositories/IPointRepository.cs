using ErrorOr;
using MuiCharts.Domain.Models;

namespace MuiCharts.Domain.Repositories;

/// <summary>
/// Represents a repository for managing points.
/// </summary>
public interface IPointRepository
{
	/// <summary>
	/// Adds a new point asynchronously.
	/// </summary>
	/// <param name="point">The point to add.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the added point or an error.</returns>
	Task<ErrorOr<Point>> AddPointAsync(Point point);

	/// <summary>
	/// Retrieves a point by its ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the point to retrieve.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the retrieved point or an error.</returns>
	Task<ErrorOr<Point>> GetPointAsync(int id);

	/// <summary>
	/// Retrieves a range of points asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains a queryable collection of points.</returns>
	Task<IQueryable<Point>> GetPointsRangeAsync();

	/// <summary>
	/// Adds or updates a point asynchronously.
	/// </summary>
	/// <param name="point">The point to add or update.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the added or updated point or an error.</returns>
	Task<ErrorOr<Point?>> AddOrUpdatePointAsync(Point point);

	/// <summary>
	/// Deletes a point by its ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the point to delete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a flag indicating if the point was deleted successfully or an error.</returns>
	Task<ErrorOr<Deleted>> DeletePointAsync(int id);
}
