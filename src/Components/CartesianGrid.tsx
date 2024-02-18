import { grey } from "@mui/material/colors";
import { styled } from "@mui/system";
import { useDrawingArea, useXScale, useYScale } from "@mui/x-charts";
import { ScaleLinear } from "d3-scale";
import React, { useEffect } from "react";

interface IProps
{
	/** The maximum Y value. */
	maxY: number;
	/** The X ticks. */
	xTicks: number[];
}

/** The Cartesian grid. */
function CartesianGrid(props: IProps): JSX.Element
{
	const [yTicks, setYTicks] = React.useState<number[]>([]);
	const xTicks = props.xTicks;

	// Get the drawing area bounding box
	const { left, top, width, height } = useDrawingArea();

	// Get the two scale
	const yAxisScale = useYScale() as ScaleLinear<unknown, number>;
	const xAxisScale = useXScale() as ScaleLinear<unknown, number>;


	useEffect(() =>
	{
		const ticks: number[] = [];

		for (let i = 1; i <= Math.ceil(props.maxY / 20) + 1; i++)
			ticks.push(i * 20);

		setYTicks(ticks);
	}, [props.maxY]);

	return (
		<React.Fragment>

			{ yTicks.map((value) =>
				<StyledPath key={ value } d={ `M ${left} ${yAxisScale(value)} l ${width} 0` } />
			) }

			{ xTicks.map((value) =>
				<StyledPath key={ value } d={ `M ${xAxisScale(value)} ${top} l 0 ${height}` } />
			) }

		</React.Fragment>
	);
}

const StyledPath = styled("path")(
	() => ({
		fill: "none",
		stroke: grey[500],
		strokeWidth: 1,
		pointerEvents: "none",
	}),
);

export default CartesianGrid;
