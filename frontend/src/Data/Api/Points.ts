import IGetPointsResponse from "./Contracts/Point/IGetPointsResponse";
import UpsertPointRequest from "./Contracts/Point/UpsertPointRequest";
import IPoint from "./Models/IPoint";

export default class Points
{
	private apiUrl: string;

	constructor(apiUrl: string)
	{
		this.apiUrl = apiUrl;
	}

	public async CreatePointAsync(request: UpsertPointRequest): Promise<IPoint>
	{
		const response: Response = await fetch(this.apiUrl + "/points", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(request)
		});

		const data: IPoint = await response.json() as IPoint;

		return data;
	}

	public async GetPointsArrayAsync(ids: number[]): Promise<IPoint[]>
	{
		const response: Response = await fetch(this.apiUrl + "/points/array", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(ids)
		});

		const data: IPoint[] = await response.json() as IPoint[];

		return data;
	}

	public async GetPointsAsync(page?: number, count?: number): Promise<IGetPointsResponse>
	{
		const params = new URLSearchParams();

		if (page)
			params.append("page", page.toString());

		if (count)
			params.append("count", count.toString());

		const response: Response = await fetch(this.apiUrl + `/points?${params.toString()}`);
		const data: IGetPointsResponse = await response.json() as IGetPointsResponse;

		return data;
	}

	public async GetPointAsync(id: number): Promise<IPoint>
	{
		const response: Response = await fetch(this.apiUrl + `/points/${id}`);
		const data: IPoint = await response.json() as IPoint;

		return data;
	}

	public async UpsertPointAsync(id: number, request: UpsertPointRequest): Promise<IPoint | null>
	{
		const response: Response = await fetch(this.apiUrl + `/points/${id}`, {
			method: "PUT",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(request)
		});

		if (response.status === 204)
			return null;

		const data: IPoint = await response.json() as IPoint;

		return data;
	}

	public async DeletePointAsync(id: number): Promise<void>
	{
		await fetch(this.apiUrl + `/points/${id}`, { method: "DELETE" });
	}

	public async ImportPointsAsync(points: IPoint[]): Promise<IPoint[]>
	{
		const response: Response = await fetch(this.apiUrl + "/points/import", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify(points)
		});

		const data: IPoint[] = await response.json() as IPoint[];

		return data;
	}
}
