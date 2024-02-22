import { SxProps } from "@mui/system";
import { LinePlot, useDrawingArea, useXScale } from "@mui/x-charts";
import { ScaleLinear } from "d3-scale";
import { useEffect } from "react";
import MaxSpeed from "../Data/MaxSpeed";
import IChartPoint from "../Data/IChartPoint";

// Remarks:
// Even though this component adds nothing to the chart and only calculates masks,
// it is still required since a) I can't use `useDrawingArea` inside `TrackChart`
// and b) I can't pass calculates styles here

interface IProps
{
	/** Data to plot. */
	dataset: IChartPoint[];
	/** Callback to update the styles. */
	onStylesUpdated: (styles: SxProps) => void;
	/** The width of the line. */
	strokeWidth?: number;
}

/** A plot of the track line. */
function TrackLinePlot({ dataset, onStylesUpdated, strokeWidth }: IProps): JSX.Element
{
	const area = useDrawingArea();
	const xAxisScale = useXScale() as ScaleLinear<unknown, number>;

	useEffect(() =>
	{
		const masks: string[] = [];

		for (let i = 0; i < Object.keys(MaxSpeed).length / 2; i++)
			masks.push("M 0 0 ");

		for (const point of dataset)
		{
			if (point.length < 1)
				continue;

			const xStart: number = xAxisScale(point.distance) - area.left;
			const xEnd: number = xAxisScale(point.distance + point.length) - area.left;

			masks[point.maxSpeed] += `H ${xStart} V ${area.height} H ${isNaN(xEnd) ? area.width : xEnd} V 0 `;
		}

		let sx: SxProps = {};

		for (let i = 0; i < masks.length; i++)
			sx = {
				...sx,
				[`& .MuiLineElement-series-${MaxSpeed[i]}`]:
				{
					clipPath: `path("${masks[i]} Z")`,
					strokeWidth: strokeWidth ?? 2,
				}
			};

		onStylesUpdated(sx);

		// Suppressing warning, since adding useXScale to the dependency array would cause an infinite component reflow
		// `area` dependency is sufficient.
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [area, dataset, strokeWidth]);

	return (
		<LinePlot />
	);
}

export default TrackLinePlot;
