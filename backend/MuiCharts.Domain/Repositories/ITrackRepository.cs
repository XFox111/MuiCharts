using ErrorOr;
using MuiCharts.Domain.Models;

namespace MuiCharts.Domain.Repositories;

/// <summary>
/// Represents a repository for managing tracks.
/// </summary>
public interface ITrackRepository
{
	/// <summary>
	/// Adds a new track asynchronously.
	/// </summary>
	/// <param name="track">The track to add.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the added track if successful, or an error if unsuccessful.</returns>
	Task<ErrorOr<Track>> AddTrackAsync(Track track);

	/// <summary>
	/// Retrieves a track asynchronously based on the specified IDs.
	/// </summary>
	/// <param name="firstId">The first ID.</param>
	/// <param name="secondId">The second ID.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the retrieved track if successful, or an error if unsuccessful.</returns>
	Task<ErrorOr<Track>> GetTrackAsync(int firstId, int secondId);

	/// <summary>
	/// Retrieves a range of tracks asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains the range of tracks.</returns>
	Task<IQueryable<Track>> GetTracksRangeAsync();

	/// <summary>
	/// Adds or updates a track asynchronously.
	/// </summary>
	/// <param name="track">The track to add or update.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the added or updated track if successful, or an error if unsuccessful.</returns>
	Task<ErrorOr<Track?>> AddOrUpdateTrackAsync(Track track);

	/// <summary>
	/// Deletes a track asynchronously based on the specified IDs.
	/// </summary>
	/// <param name="firstId">The first ID.</param>
	/// <param name="secondId">The second ID.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the deletion status if successful, or an error if unsuccessful.</returns>
	Task<ErrorOr<Deleted>> DeleteTrackAsync(int firstId, int secondId);

	/// <summary>
	/// Adds a range of tracks asynchronously.
	/// </summary>
	/// <param name="tracks">The tracks to add.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the added tracks if successful, or an error if unsuccessful.</returns>
	Task<ErrorOr<IEnumerable<Track>>> AddTracksRangeAsync(IEnumerable<Track> tracks);
}
