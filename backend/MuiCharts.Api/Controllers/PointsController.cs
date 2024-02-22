using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MuiCharts.Contracts.Point;
using MuiCharts.Domain.Models;
using MuiCharts.Domain.Repositories;

namespace MuiCharts.Api.Controllers;

/// <summary>
/// Controller for managing points.
/// </summary>
public class PointsController(
	ILogger<PointsController> logger,
	IPointRepository pointRepository
) : ApiControllerBase<PointsController>(logger)
{
	private readonly IPointRepository _repository = pointRepository;

	/// <summary>
	/// Creates a new point.
	/// </summary>
	/// <param name="request">The new point model.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpPost]
	[ProducesResponseType<Point>(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> CreatePointAsync(UpsertPointRequest request)
	{
		Logger.LogInformation("Creating point with name {Name} and height {Height}", request.Name, request.Height);

		Point point = new()
		{
			Id = default,
			Name = request.Name,
			Height = request.Height
		};

		ErrorOr<Point> createResult = await _repository.AddPointAsync(point);

		if (createResult.IsError)
			return Problem(createResult.Errors);

		Logger.LogInformation("Point created with id {Id}", createResult.Value.Id);

		return CreatedAtPointResult(createResult.Value);
	}

	/// <summary>
	/// Retrieves an array of points based on the provided IDs.
	/// </summary>
	/// <param name="ids">The array of point IDs.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpPost("Array")]
	[ProducesResponseType<Point[]>(StatusCodes.Status200OK)]
	[ProducesResponseType<Point[]>(StatusCodes.Status206PartialContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> GetPointsArrayAsync(int[] ids)
	{
		Logger.LogInformation("Getting points with ids {Ids}", ids);

		IQueryable<Point> query = await _repository.GetPointsRangeAsync();

		PointResponse[] points = [
			.. query
				.Where(point => ids.Contains(point.Id))
				.Select(point => MapPointResponse(point))
		];

		if (points.Length == 0)
		{
			Logger.LogInformation("No points found with ids {Ids}", ids);
			return NotFound();
		}

		if (points.Length != ids.Length)
		{
			Logger.LogInformation("Not all points found with ids {Ids}", ids);
			return StatusCode(StatusCodes.Status206PartialContent, points);
		}

		Logger.LogInformation("Returning {Count} points", points.Length);

		return Ok(points);
	}

	/// <summary>
	/// Retrieves a range of points based on the specified page and count.
	/// </summary>
	/// <param name="request">The request object containing the page and count parameters.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpGet]
	[ProducesResponseType<GetPointsResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> GetPointsAsync([FromQuery] GetPointsRequest request)
	{
		Logger.LogInformation("Getting points with page {Page} and count {Count}", request.Page, request.Count);

		IQueryable<Point> query = await _repository.GetPointsRangeAsync();

		PointResponse[] points = [
			.. query
			.Skip((request.Page - 1) * request.Count)
			.Take(request.Count)
			.Select(point => MapPointResponse(point))
		];

		GetPointsResponse response = new(
			points,
			query.Count(),
			points.Length,
			request.Page
		);

		Logger.LogInformation("Returning {Count} points", response.Count);

		return Ok(response);
	}

	/// <summary>
	/// Retrieves a point with the specified ID.
	/// </summary>
	/// <param name="id">The ID of the point to retrieve.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpGet("{id:int}")]
	[ProducesResponseType<Point>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> GetPointAsync(int id)
	{
		Logger.LogInformation("Getting point with id {Id}", id);

		ErrorOr<Point> getResult = await _repository.GetPointAsync(id);

		if (getResult.IsError)
			return Problem(getResult.Errors);

		Logger.LogInformation("Returning point with id {Id}", id);

		return Ok(MapPointResponse(getResult.Value));
	}

	/// <summary>
	/// Upserts a point with the specified ID and request data.
	/// </summary>
	/// <param name="id">The ID of the point.</param>
	/// <param name="request">The request data for the point.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpPut("{id:int}")]
	[ProducesResponseType<Point>(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> UpsertPointAsync(int id, UpsertPointRequest request)
	{
		Logger.LogInformation("Upserting point with id {Id}", id);

		Point point = new()
		{
			Id = id,
			Name = request.Name,
			Height = request.Height
		};

		ErrorOr<Point?> upsertResult = await _repository.AddOrUpdatePointAsync(point);

		if (upsertResult.IsError)
			return Problem(upsertResult.Errors);

		if (upsertResult.Value is Point value)
		{
			Logger.LogInformation("Point created with id {Id}", value.Id);
			return CreatedAtPointResult(value);
		}

		Logger.LogInformation("Point updated with id {Id}", id);
		return NoContent();
	}

	/// <summary>
	/// Deletes a point with the specified ID.
	/// </summary>
	/// <param name="id">The ID of the point to delete.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> DeletePointAsync(int id)
	{
		Logger.LogInformation("Deleting point with id {Id}", id);

		ErrorOr<Deleted> deleteResult = await _repository.DeletePointAsync(id);

		if (deleteResult.IsError)
			return Problem(deleteResult.Errors);

		Logger.LogInformation("Point deleted with id {Id}", id);

		return NoContent();
	}

	private CreatedAtActionResult CreatedAtPointResult(Point point) =>
		CreatedAtAction(
			actionName: nameof(GetPointAsync),
			routeValues: new { id = point.Id },
			value: MapPointResponse(point)
		);

	private static PointResponse MapPointResponse(Point value) =>
		new(
			value.Id,
			value.Name,
			value.Height
		);
}
