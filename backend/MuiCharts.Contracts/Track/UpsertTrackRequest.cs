using System.ComponentModel.DataAnnotations;
using MuiCharts.Domain.Enums;

namespace MuiCharts.Contracts.Track;

/// <summary>
/// Represents a request to upsert a track.
/// </summary>
public record UpsertTrackRequest(
	[Range(0, int.MaxValue)] int FirstId,
	[Range(0, int.MaxValue)] int SecondId,
	[Range(1, int.MaxValue)] int Distance,
	Surface Surface,
	MaxSpeed MaxSpeed
);
