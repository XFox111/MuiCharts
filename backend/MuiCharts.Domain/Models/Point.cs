using System.ComponentModel.DataAnnotations;

namespace MuiCharts.Domain.Models;

/// <summary>
/// Represents a point with an ID, name, and height.
/// </summary>
public class Point
{
	/// <summary>
	/// Gets or sets the ID of the point.
	/// </summary>
	public required int Id { get; init; }

	/// <summary>
	/// Gets or sets the name of the point.
	/// </summary>
	[MinLength(1)]
	public required string Name { get; init; }

	/// <summary>
	/// Gets or sets the height of the point.
	/// </summary>
	public required int Height { get; init; }
}
