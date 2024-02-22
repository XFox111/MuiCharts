import { SxProps } from "@mui/system";
import * as xc from "@mui/x-charts";
import { useEffect, useState } from "react";
import IChartPoint from "../Data/IChartPoint";
import IPoint from "../Data/IPoint";
import ITrack from "../Data/ITrack";
import MaxSpeed from "../Data/MaxSpeed";
import { TooltipProps, TracklineSeries } from "../Data/TrackChartDataProps";
import CartesianGrid from "./CartesianGrid";
import TrackLinePlot from "./TrackLinePlot";
import TrackSurfacePlot from "./TrackSurfacePlot";

interface IProps
{
	/** The tracks data. */
	tracks: ITrack[];
	/** The points data. */
	points: IPoint[];
	/** The zoom levels (start, end). */
	zoom: number[];
	/** A callback for when the processing is complete. */
	onProcessingComplete?: () => void;
}

/**
 * A chart of the track.
 */
function TrackChart({ tracks, points, zoom, ...props }: IProps): JSX.Element
{
	const [dataset, setDataset] = useState<IChartPoint[]>([]);
	const [xTicks, setXTicks] = useState<number[]>([]);
	const [zoomedDataset, setZoomedDataset] = useState<IChartPoint[]>([]);
	const [zoomedXTicks, setZoomedXTicks] = useState<number[]>([]);
	const [maskStyles, setMasks] = useState<SxProps>({});
	const maxPointHeight: number = Math.max(...points.map(i => i.height));

	useEffect(() =>
	{
		for (let i = 0; i < dataset.length; i++)
		{
			const selector: string = `.MuiMarkElement-series-${MaxSpeed[dataset[i].maxSpeed]}`;
			const element: SVGPathElement | undefined = document.querySelectorAll<SVGPathElement>(selector)[i];
			// eslint-disable-next-line @typescript-eslint/no-unnecessary-condition
			element?.style.setProperty("display", "inline");
		}
	});

	useEffect(() =>
	{
		if (tracks.length < 1 || points.length < 1)
			return;

		const data: IChartPoint[] = [];
		let currentDistance: number = 0;

		for (const track of tracks)
		{
			const firstPoint = points.find(p => p.id === track.firstId)!;

			data.push({
				distance: currentDistance,
				length: track.distance,
				surface: track.surface,
				maxSpeed: track.maxSpeed,
				name: `${firstPoint.name} (ID#${firstPoint.id})`,
				height: firstPoint.height,
			});

			currentDistance += track.distance;
		}

		const lastPoint = points.find(p => p.id === tracks[tracks.length - 1].secondId)!;

		data.push({
			distance: currentDistance,
			length: -1,
			surface: tracks[tracks.length - 1].surface,
			maxSpeed: tracks[tracks.length - 1].maxSpeed,
			name: `${lastPoint.name} (ID#${lastPoint.id})`,
			height: lastPoint.height
		});

		setDataset(data);
		setXTicks(data.map(i => i.distance));

		console.warn("Reflow!");

		setTimeout(() => props.onProcessingComplete?.(), 500);
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [tracks, points, props.onProcessingComplete]);

	useEffect(() =>
	{
		setZoomedDataset(dataset.slice(zoom[0], zoom[1]));
		setZoomedXTicks(xTicks.slice(zoom[0], zoom[1]));
	}, [dataset, xTicks, zoom]);

	const getSx = (): SxProps => ({
		"& .MuiMarkElement-root":
		{
			display: "none",
		},
		...maskStyles,
	});

	return (
		<xc.ResponsiveChartContainer
			dataset={ zoomedDataset }
			sx={ getSx() }
			yAxis={ [{ max: (Math.ceil(maxPointHeight / 20) + 1) * 20, min: 0 }] }
			xAxis={ [{ dataKey: "distance", scaleType: "point" }] }
			series={ TracklineSeries }>

			<TrackSurfacePlot dataset={ zoomedDataset } />
			<CartesianGrid maxY={ maxPointHeight } xTicks={ zoomedXTicks } />
			<TrackLinePlot dataset={ zoomedDataset } onStylesUpdated={ styles => setMasks(styles) } strokeWidth={ 4 } />

			<xc.ChartsXAxis />
			<xc.ChartsYAxis />
			<xc.ChartsAxisHighlight x="line" />
			<xc.MarkPlot />
			<xc.LineHighlightPlot />

			<xc.ChartsTooltip
				slotProps={ {
					axisContent: TooltipProps(zoomedDataset)
				} } />

		</xc.ResponsiveChartContainer>
	);
}

export default TrackChart;
