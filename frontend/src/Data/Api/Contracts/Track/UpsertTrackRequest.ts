import MaxSpeed from "../../Models/MaxSpeed";
import Surface from "../../Models/Surface";

export default class UpsertTrackRequest
{
	public firstId: number;
	public secondId: number;
	public distance: number;
	public surface: Surface;
	public maxSpeed: MaxSpeed;

	constructor(firstId: number, secondId: number, distance: number, surface: number, maxSpeed: number)
	{
		if (firstId < 0)
			throw new Error("First ID must be at least 0");

		if (secondId < 0)
			throw new Error("Second ID must be at least 0");

		if (distance < 1)
			throw new Error("Distance must be greater than 0");

		this.firstId = firstId;
		this.secondId = secondId;
		this.distance = distance;
		this.surface = surface;
		this.maxSpeed = maxSpeed;
	}
}
