import MaxSpeed from "./MaxSpeed";
import Surface from "./Surface";

/** Represents a track segment */
export default interface ITrack
{
	/** First point ID */
	firstId: number;

	/** Second (last) point ID */
	secondId: number;

	/** Distance between points */
	distance: number;

	/** Surface type */
	surface: Surface;

	/** Maximum speed */
	maxSpeed: MaxSpeed;
}
