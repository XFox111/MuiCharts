using System.ComponentModel.DataAnnotations;
using MuiCharts.Domain.Enums;

namespace MuiCharts.Domain.Models;

/// <summary>
/// Represents a track with information about the first point ID, second point ID, distance, surface, and maximum speed.
/// </summary>
public class Track
{
	/// <summary>
	/// Gets or sets the first ID.
	/// </summary>
	public required int FirstId { get; init; }

	/// <summary>
	/// Gets or sets the second ID.
	/// </summary>
	public required int SecondId { get; init; }

	/// <summary>
	/// Gets or sets the distance.
	/// </summary>
	[Range(1, int.MaxValue)]
	public required int Distance { get; init; }

	/// <summary>
	/// Gets or sets the surface.
	/// </summary>
	public required Surface Surface { get; init; }

	/// <summary>
	/// Gets or sets the maximum speed.
	/// </summary>
	public required MaxSpeed MaxSpeed { get; init; }
}
