import UpsertTrackRequest from "./Contracts/Track/UpsertTrackRequest";
import ITrack from "./Models/ITrack";

export default class Tracks
{
	private apiUrl: string;

	constructor(apiUrl: string)
	{
		this.apiUrl = apiUrl;
	}

	public async CreateTrackAsync(request: UpsertTrackRequest): Promise<ITrack>
	{
		const response: Response = await fetch(this.apiUrl + "/tracks", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(request)
		});

		const data: ITrack = await response.json() as ITrack;

		return data;
	}

	public async GetTrackAsync(firstId: number, secondId: number): Promise<ITrack>
	{
		const response: Response = await fetch(this.apiUrl + `/tracks/${firstId}/${secondId}`);
		const data: ITrack = await response.json() as ITrack;

		return data;
	}

	public async GetAllTracksAsync(): Promise<ITrack[]>
	{
		const response: Response = await fetch(this.apiUrl + "/tracks");
		const data: ITrack[] = await response.json() as ITrack[];

		return data;
	}

	public async UpsertTrackAsync(
		firstId: number,
		secondId: number,
		request: UpsertTrackRequest
	): Promise<ITrack | null>
	{
		const response: Response = await fetch(this.apiUrl + `/tracks/${firstId}/${secondId}`, {
			method: "PUT",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(request)
		});

		if (response.status === 204)
			return null;

		const data: ITrack = await response.json() as ITrack;

		return data;
	}

	public async DeleteTrackAsync(firstId: number, secondId: number): Promise<void>
	{
		await fetch(this.apiUrl + `/tracks/${firstId}/${secondId}`, {
			method: "DELETE"
		});
	}

	public async ImportTracksAsync(tracks: ITrack[]): Promise<ITrack[]>
	{
		const response: Response = await fetch(this.apiUrl + "/tracks/import", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(tracks)
		});

		const data: ITrack[] = await response.json() as ITrack[];

		return data;
	}
}
