export default class UpsertPointRequest
{
	public name: string;
	public height: number;

	constructor(name: string, height: number)
	{
		if (name.length < 1)
			throw new Error("Name must be at least 1 character long");

		if (height !== Math.floor(height))
			throw new Error("Height must be an integer");

		this.name = name;
		this.height = height;
	}
}
