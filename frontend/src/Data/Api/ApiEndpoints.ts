import Points from "./Points";
import Tracks from "./Tracks";

const apiUrl: string = import.meta.env.VITE_API_URL as string;

export default class ApiEndpoints
{
	public Points: Points;
	public Tracks: Tracks;

	constructor()
	{
		this.Points = new Points(apiUrl);
		this.Tracks = new Tracks(apiUrl);
	}
}
