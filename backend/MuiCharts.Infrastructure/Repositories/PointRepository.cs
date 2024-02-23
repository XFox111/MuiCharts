using ErrorOr;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using MuiCharts.Domain.Models;
using MuiCharts.Domain.Repositories;

namespace MuiCharts.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for points.
/// </summary>
/// <param name="context">The data context.</param>
/// <param name="logger">The logger.</param>
public class PointRepository(
	DataContext context,
	ILogger<PointRepository> logger
) : IPointRepository
{
	private readonly DataContext _context = context;
	private readonly ILogger<PointRepository> _logger = logger;

	/// <inheritdoc/>
	public async Task<ErrorOr<Point?>> AddOrUpdatePointAsync(Point point)
	{
		try
		{
			_logger.LogInformation("Adding or updating point {point}", point);

			Point result = UpsertPoint(point, out bool isNewlyCreated);
			await _context.SaveChangesAsync();

			if (!isNewlyCreated)
				return (Point?)null;

			return result;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error adding or updating point {point}", point);
			return Error.Failure();
		}
	}

	/// <inheritdoc/>
	public async Task<ErrorOr<Point>> AddPointAsync(Point point)
	{
		try
		{
			_logger.LogInformation("Adding or updating point {point}", point);
			EntityEntry<Point> result = await _context.Points.AddAsync(point);
			await _context.SaveChangesAsync();

			return result.Entity;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error adding point {point}", point);
			return Error.Failure();
		}
	}

	public async Task<ErrorOr<IEnumerable<Point>>> AddPointsRangeAsync(IEnumerable<Point> points)
	{
		try
		{
			_logger.LogInformation("Adding points rage");

			List<Point> updatedPoints = [];

			foreach (Point point in points)
				updatedPoints.Add(UpsertPoint(point, out _));

			await _context.SaveChangesAsync();
			_logger.LogInformation("Added {Count} points", updatedPoints.Count);

			return updatedPoints;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error adding points {points}", points);
			return Error.Failure();
		}
	}

	/// <inheritdoc/>
	public async Task<ErrorOr<Deleted>> DeletePointAsync(int id)
	{
		try
		{
			_logger.LogInformation("Deleting point {id}", id);

			Point? point = await _context.Points.FindAsync(id);

			if (point == null)
				return Error.NotFound();

			if (_context.Tracks.Any(t => t.FirstId == id || t.SecondId == id))
				return Error.Conflict(description: "Point is used in a track. Delete track first");

			_context.Points.Remove(point);
			await _context.SaveChangesAsync();

			return Result.Deleted;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error deleting point {id}", id);
			return Error.Failure();
		}
	}

	/// <inheritdoc/>
	public async Task<ErrorOr<Point>> GetPointAsync(int id)
	{
		try
		{
			_logger.LogInformation("Getting point {id}", id);

			Point? point = await _context.Points.FindAsync(id);

			if (point == null)
				return Error.NotFound();

			return point;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error getting point {id}", id);
			return Error.Failure();
		}
	}

	/// <inheritdoc/>
	public Task<IQueryable<Point>> GetPointsRangeAsync()
	{
		return Task.FromResult(_context.Points.AsQueryable());
	}

	private Point UpsertPoint(Point point, out bool isNewlyCreated)
	{
		bool doesExist = _context.Points.Any(p => p.Id == point.Id);
		isNewlyCreated = !doesExist;
		Point entity;

		if (doesExist)
			entity = _context.Points.Update(point).Entity;
		else
			entity = _context.Points.Add(point).Entity;

		return entity;
	}
}
