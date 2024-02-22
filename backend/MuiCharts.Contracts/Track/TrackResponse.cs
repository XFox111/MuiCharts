using MuiCharts.Domain.Enums;

namespace MuiCharts.Contracts.Track;

/// <summary>
/// Represents a response object for a track.
/// </summary>
public record TrackResponse(
	int FirstId,
	int SecondId,
	int Distance,
	Surface Surface,
	MaxSpeed MaxSpeed
);
