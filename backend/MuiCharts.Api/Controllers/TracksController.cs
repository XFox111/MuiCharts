using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MuiCharts.Contracts.Track;
using MuiCharts.Domain.Models;
using MuiCharts.Domain.Repositories;

namespace MuiCharts.Api.Controllers;

/// <summary>
/// Controller for managing tracks.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="trackRepository">The track repository.</param>
public class TracksController(
	ILogger<TracksController> logger,
	ITrackRepository trackRepository
) : ApiControllerBase<TracksController>(logger)
{
	private readonly ITrackRepository _repository = trackRepository;

	/// <summary>
	/// Creates a new track.
	/// </summary>
	/// <param name="request">The request containing the track details.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpPost]
	[ProducesResponseType<TrackResponse>(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> CreateTrackAsync(UpsertTrackRequest request)
	{
		// TODO: Check if points exist
		Logger.LogInformation("Creating track with first ID {FirstId} and second ID {SecondId}", request.FirstId, request.SecondId);

		if (request.FirstId == request.SecondId)
			return Problem([Error.Validation(description: "First ID and second ID cannot be the same.")]);

		Track track = new()
		{
			FirstId = request.FirstId,
			SecondId = request.SecondId,
			Distance = request.Distance,
			Surface = request.Surface,
			MaxSpeed = request.MaxSpeed
		};

		ErrorOr<Track> createResult = await _repository.AddTrackAsync(track);

		if (createResult.IsError)
			return Problem(createResult.Errors);

		Logger.LogInformation("Track created with first ID {FirstId} and second ID {SecondId}", createResult.Value.FirstId, createResult.Value.SecondId);

		return CreatedAtTrackResult(createResult.Value);
	}

	/// <summary>
	/// Retrieves a track with the specified first ID and second ID.
	/// </summary>
	/// <param name="firstId">The first point ID of the track.</param>
	/// <param name="secondId">The second point ID of the track.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpGet("{firstId:int}/{secondId:int}")]
	[ProducesResponseType<TrackResponse>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> GetTrackAsync(int firstId, int secondId)
	{
		Logger.LogInformation("Retrieving track with first ID {FirstId} and second ID {SecondId}", firstId, secondId);

		ErrorOr<Track> getResult = await _repository.GetTrackAsync(firstId, secondId);

		if (getResult.IsError)
			return Problem(getResult.Errors);

		Logger.LogInformation("Track retrieved with first ID {FirstId} and second ID {SecondId}", getResult.Value.FirstId, getResult.Value.SecondId);

		return Ok(MapTrackResponse(getResult.Value));
	}

	/// <summary>
	/// Retrieves all tracks.
	/// </summary>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpGet]
	[ProducesResponseType<IEnumerable<TrackResponse>>(StatusCodes.Status200OK)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> GetAllTracksAsync()
	{
		Logger.LogInformation("Retrieving all tracks");

		IQueryable<Track> tracks = await _repository.GetTracksRangeAsync();

		Logger.LogInformation("All tracks retrieved");

		return Ok(tracks.Select(MapTrackResponse));
	}

	/// <summary>
	/// Upserts a track with the specified first point ID, second point ID, and request data.
	/// </summary>
	/// <param name="firstId">The first point ID of the track.</param>
	/// <param name="secondId">The second point ID of the track.</param>
	/// <param name="request">The request data containing the track details.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpPut("{firstId:int}/{secondId:int}")]
	[ProducesResponseType<TrackResponse>(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> UpsertTrackAsync(int firstId, int secondId, UpsertTrackRequest request)
	{
		// TODO: Check if points exist
		Logger.LogInformation("Upserting track with first ID {FirstId} and second ID {SecondId}", firstId, secondId);

		Track track = new()
		{
			FirstId = firstId,
			SecondId = secondId,
			Distance = request.Distance,
			Surface = request.Surface,
			MaxSpeed = request.MaxSpeed
		};

		ErrorOr<Track?> upsertResult = await _repository.AddOrUpdateTrackAsync(track);

		if (upsertResult.IsError)
			return Problem(upsertResult.Errors);

		if (upsertResult.Value is Track value)
		{
			Logger.LogInformation("Track created with first ID {FirstId} and second ID {SecondId}", value.FirstId, value.SecondId);
			return CreatedAtTrackResult(value);
		}

		Logger.LogInformation("Track updated with first ID {FirstId} and second ID {SecondId}", firstId, secondId);
		return NoContent();
	}

	/// <summary>
	/// Deletes a track with the specified first point ID and second point ID.
	/// </summary>
	/// <param name="firstId">The first point ID of the track to delete.</param>
	/// <param name="secondId">The second point ID of the track to delete.</param>
	/// <returns>An <see cref="IActionResult"/> representing the asynchronous operation result.</returns>
	[HttpDelete("{firstId:int}/{secondId:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType(typeof(ProblemDetails))]
	public async Task<IActionResult> DeleteTrackAsync(int firstId, int secondId)
	{
		Logger.LogInformation("Deleting track with first ID {FirstId} and second ID {SecondId}", firstId, secondId);

		ErrorOr<Deleted> deleteResult = await _repository.DeleteTrackAsync(firstId, secondId);

		if (deleteResult.IsError)
			return Problem(deleteResult.Errors);

		Logger.LogInformation("Track deleted with first ID {FirstId} and second ID {SecondId}", firstId, secondId);

		return NoContent();
	}

	private CreatedAtActionResult CreatedAtTrackResult(Track track) =>
		CreatedAtAction(
			actionName: nameof(GetTrackAsync),
			routeValues: new { firstId = track.FirstId, secondId = track.SecondId },
			value: MapTrackResponse(track)
		);

	private static TrackResponse MapTrackResponse(Track track) =>
		new(
			track.FirstId,
			track.SecondId,
			track.Distance,
			track.Surface,
			track.MaxSpeed
		);
}
