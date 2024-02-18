import IPoint from "./IPoint";
import ITrack from "./ITrack";

/**
 * Returns a random number between min and max
 * @param min The minimum value (inclusive)
 * @param max The maximum value (exclusive)
 * @returns A random number between min and max
 */
const getRandom = (min: number, max: number): number =>
	Math.floor(Math.random() * (max - min)) + min;

const pointNames: string[] =
	[
		"Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India",
		"Juliett", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo",
		"Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu"
	];

function LoadMockData(largeCount?: boolean): { tracks: ITrack[], points: IPoint[]; }
{
	const count: number = getRandom(10, 20) * (largeCount ? 10 : 1);
	const points: IPoint[] = [];
	const tracks: ITrack[] = [];

	// Generate random data

	for (let i = 0; i < count; i++)
	{
		points.push({
			id: i,
			name: `${pointNames[getRandom(0, pointNames.length)]}-${i}`,
			height: getRandom(50, 200)
		});
	}

	for (let i = 0; i < count - 1; i++)
	{
		tracks.push({
			firstId: points[i].id,
			secondId: points[i + 1].id,
			distance: getRandom(1000, 2000),
			surface: getRandom(0, 3),
			maxSpeed: getRandom(0, 3)
		});
	}

	return { tracks, points };
}

export default LoadMockData;
