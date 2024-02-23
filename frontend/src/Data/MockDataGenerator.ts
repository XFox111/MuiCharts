import IPoint from "./Api/Models/IPoint";
import ITrack from "./Api/Models/ITrack";
import MaxSpeed from "./Api/Models/MaxSpeed";
import Surface from "./Api/Models/Surface";
import GaussianGenerator from "./GaussianGenerator";

export function GeneratePoints(count: number): IPoint[]
{
	const generator = new GaussianGenerator(160, 10);

	const points: IPoint[] = generator.Generate(count).map((height, index) => ({
		id: index + 1,
		name: `${pointNames[index % pointNames.length]}-${index + 1}`,
		height
	}));

	return points;
}

export function GenerateTracks(points: IPoint[]): ITrack[]
{
	const generator = new GaussianGenerator(2000, 500);

	const tracks: ITrack[] = generator.Generate(points.length - 1).map((distance, index) => ({
		firstId: points[index].id,
		secondId: points[index + 1].id,
		distance,
		surface: getRandom(0, 3) as Surface,
		maxSpeed: getRandom(0, 3) as MaxSpeed
	}));

	return tracks;
}

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
