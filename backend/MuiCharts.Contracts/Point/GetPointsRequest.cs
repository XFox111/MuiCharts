using System.ComponentModel.DataAnnotations;

namespace MuiCharts.Contracts.Point;

/// <summary>
/// Represents a request to get a collection of points.
/// </summary>
public record class GetPointsRequest(
	[Range(1, int.MaxValue)]
	int Page = 1,
	[Range(1, int.MaxValue)]
	int Count = 50
);
