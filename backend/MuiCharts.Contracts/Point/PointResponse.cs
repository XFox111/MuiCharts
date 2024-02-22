namespace MuiCharts.Contracts.Point;

/// <summary>
/// Represents a response object containing information about a point.
/// </summary>
public record PointResponse(
	int Id,
	string Name,
	int Height
);
