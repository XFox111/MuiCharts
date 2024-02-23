import IPoint from "../../Models/IPoint";

export default interface IGetPointsResponse
{
	points: IPoint[],
	totalCount: number,
	count: number,
	page: number
}
