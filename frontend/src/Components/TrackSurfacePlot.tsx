import { styled } from "@mui/material";
import { useDrawingArea, useXScale } from "@mui/x-charts";
import { ScaleLinear } from "d3-scale";
import React from "react";
import IChartPoint from "../Data/IChartPoint";
import { SurfaceColors } from "../Data/TrackChartDataProps";

interface IProps
{
	/** Data to plot. */
	dataset: IChartPoint[];
}

/** A plot of the track surface. */
function TrackSurfacePlot({ dataset }: IProps): JSX.Element
{
	// Get the drawing area bounding box
	const { top, height } = useDrawingArea();
	const xAxisScale = useXScale() as ScaleLinear<unknown, number>;

	// Calculate the width of each track
	const getWidth = (item: IChartPoint): number =>
	{
		const width = xAxisScale(item.distance + item.length) - xAxisScale(item.distance);
		return isNaN(width) ? 0 : width;
	}

	return (
		<React.Fragment>
			{ dataset.map((i, index) => i.length > 0 &&
				<StyledRect key={ index }
					x={ xAxisScale(i.distance) } y={ top }
					width={ getWidth(i) } height={ height }
					color={ SurfaceColors[i.surface] } />
			) }
		</React.Fragment>
	);
}

const StyledRect = styled("rect")<{ color: string; }>(
	({ color }) => ({
		fill: color,
		pointerEvents: "none"
	})
);

export default TrackSurfacePlot;
