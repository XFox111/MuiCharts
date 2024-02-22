import MaxSpeed from "./MaxSpeed";
import Surface from "./Surface";

/** Represents an aggregated point on the chart. */
export default interface IChartPoint
{
	/** The distance from the start of the track. */
	distance: number;
	/** The distance until the next point. */
	length: number;
	/** The type of track surface */
	surface: Surface;
	/** The maximum speed of the point. */
	maxSpeed: MaxSpeed;
	/** The name of the point. */
	name: string;
	/** The height of the point. */
	height: number;
	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	[key: string]: any;
}
