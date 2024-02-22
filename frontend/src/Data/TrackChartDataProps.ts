import { green, grey, orange, red, yellow } from "@mui/material/colors";
import { AllSeriesType, ChartsAxisContentProps } from "@mui/x-charts";
import IChartPoint from "./IChartPoint";
import MaxSpeed from "./MaxSpeed";
import Surface from "./Surface";

/** Props for rendering trackline segments. */
export const TracklineSeries: AllSeriesType[] = [
	// There're three of them, because there are.
	// Instead of trying to split signle line into different segments,
	// I instead apply a mask to each line, which is a bit more efficient.
	{
		type: "line",
		dataKey: "height",
		curve: "catmullRom",
		id: MaxSpeed[0],
		color: green[500],
	},
	{
		type: "line",
		dataKey: "height",
		curve: "catmullRom",
		id: MaxSpeed[1],
		color: orange[500],
	},
	{
		type: "line",
		dataKey: "height",
		curve: "catmullRom",
		id: MaxSpeed[2],
		color: red[500],
	},
];

/** Props for the chart tooltip. */
export const TooltipProps = (dataset: IChartPoint[]): Partial<ChartsAxisContentProps> => ({
	// @ts-expect-error - The type definition is incorrect
	axis:
	{
		valueFormatter: (value: number): string => `${value} mi`
	},
	series:
		[
			{
				type: "line",
				data: dataset.map((_, index) => index),
				label: "Point name",
				id: "name",
				valueFormatter: (value: number) => dataset[value].name,
				color: green[500],
			},
			{
				type: "line",
				data: dataset.map(i => i.height),
				label: "Height ASL",
				id: "height",
				valueFormatter: (value: number) => `${value} ft`,
				color: green[500],
			},
			{
				type: "line",
				data: dataset.map(i => i.surface),
				label: "Segment type",
				id: "surface",
				valueFormatter: (value: number) => Surface[value],
				color: green[700],
			},
			{
				type: "line",
				data: dataset.map(i => i.length),
				id: "length",
				label: "Next point",
				valueFormatter: (value: number) => value < 1 ? "FINISH" : `${value} mi`,
				color: yellow[200],
			},
			{
				type: "line",
				data: dataset.map(i => i.maxSpeed),
				id: "maxSpeed",
				label: "Speed caution",
				valueFormatter: (value: number) => MaxSpeed[value],
				color: red[500],
			},
		]
});

/** Colors for each surface type. */
export const SurfaceColors: Record<Surface, string> = {
	[Surface.ASPHALT]: grey[400],
	[Surface.SAND]: yellow[100],
	[Surface.GROUND]: green[100],
};
