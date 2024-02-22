using ErrorOr;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using MuiCharts.Domain.Models;
using MuiCharts.Domain.Repositories;

namespace MuiCharts.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for tracks.
/// </summary>
/// <param name="context">The data context.</param>
/// <param name="logger">The logger.</param>
public class TrackRepository(
	DataContext context,
	ILogger<TrackRepository> logger
) : ITrackRepository
{
	private readonly DataContext _context = context;
	private readonly ILogger<TrackRepository> _logger = logger;

	/// <inheritdoc />
	public async Task<ErrorOr<Track?>> AddOrUpdateTrackAsync(Track track)
	{
		try
		{
			_logger.LogInformation("Adding or updating track {track}", track);

			if (!IsValidTrack(track))
			{
				_logger.LogInformation("Points with first ID {FirstId} and second ID {SecondId} do not exist", track.FirstId, track.SecondId);
				return Error.Validation(description: "One or both specified points do not exist.");
			}

			bool doesExist = _context.Tracks.Any(t => t.FirstId == track.FirstId && t.SecondId == track.SecondId);

			if (doesExist)
			{
				_logger.LogInformation("Track with first ID {FirstId} and second ID {SecondId} exists, updating", track.FirstId, track.SecondId);
				_context.Tracks.Update(track);
				await _context.SaveChangesAsync();
				return (Track?)null;
			}
			else
			{
				_logger.LogInformation("Track with first ID {FirstId} and second ID {SecondId} does not exist, adding", track.FirstId, track.SecondId);
				EntityEntry<Track> result = _context.Tracks.Add(track);
				await _context.SaveChangesAsync();

				return result.Entity;
			}
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error adding or updating track {track}", track);
			return Error.Failure();
		}
	}

	/// <inheritdoc />
	public async Task<ErrorOr<Track>> AddTrackAsync(Track track)
	{
		try
		{
			_logger.LogInformation("Adding track with first ID {FirstId} and second ID {SecondId}", track.FirstId, track.SecondId);

			if (!IsValidTrack(track))
			{
				_logger.LogInformation("Points with first ID {FirstId} and second ID {SecondId} do not exist", track.FirstId, track.SecondId);
				return Error.Validation(description: "One or both specified points do not exist.");
			}

			if (_context.Tracks.Any(t => t.FirstId == track.FirstId && t.SecondId == track.SecondId))
			{
				_logger.LogInformation("Track with first ID {FirstId} and second ID {SecondId} already exists", track.FirstId, track.SecondId);
				return Error.Conflict();
			}

			EntityEntry<Track> result = _context.Tracks.Add(track);
			await _context.SaveChangesAsync();

			return result.Entity;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error adding track with first ID {FirstId} and second ID {SecondId}", track.FirstId, track.SecondId);
			return Error.Failure();
		}
	}

	/// <inheritdoc />
	public async Task<ErrorOr<Deleted>> DeleteTrackAsync(int firstId, int secondId)
	{
		try
		{
			_logger.LogInformation("Deleting track with first ID {FirstId} and second ID {SecondId}", firstId, secondId);

			Track? track = await _context.Tracks.FindAsync(firstId, secondId);

			if (track is null)
			{
				_logger.LogInformation("Track with first ID {FirstId} and second ID {SecondId} does not exist", firstId, secondId);
				return Error.NotFound();
			}

			_context.Tracks.Remove(track);
			await _context.SaveChangesAsync();

			return new Deleted();
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error deleting track with first ID {FirstId} and second ID {SecondId}", firstId, secondId);
			return Error.Failure();
		}
	}

	/// <inheritdoc />
	public async Task<ErrorOr<Track>> GetTrackAsync(int firstId, int secondId)
	{
		try
		{
			_logger.LogInformation("Getting track with first ID {FirstId} and second ID {SecondId}", firstId, secondId);

			Track? track = await _context.Tracks.FindAsync(firstId, secondId);

			if (track is null)
			{
				_logger.LogInformation("Track with first ID {FirstId} and second ID {SecondId} does not exist", firstId, secondId);
				return Error.NotFound();
			}

			return track;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Error getting track with first ID {FirstId} and second ID {SecondId}", firstId, secondId);
			return Error.Failure();
		}
	}

	/// <inheritdoc />
	public Task<IQueryable<Track>> GetTracksRangeAsync()
	{
		return Task.FromResult(_context.Tracks.AsQueryable());
	}

	private bool IsValidTrack(Track track)
	{
		return _context.Points.Any(p => p.Id == track.FirstId) &&
			_context.Points.Any(p => p.Id == track.SecondId);
	}
}
