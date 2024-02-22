namespace MuiCharts.Contracts.Point;

/// <summary>
/// Represents the response object for retrieving points.
/// </summary>
public record class GetPointsResponse(
	PointResponse[] Points,
	int TotalCount,
	int Count,
	int Page
);
