using System.ComponentModel.DataAnnotations;

namespace MuiCharts.Contracts.Point;

/// <summary>
/// Represents a request to upsert a point.
/// </summary>
public record UpsertPointRequest(
	[MinLength(1)] string Name,
	int Height
);
