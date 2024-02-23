/**
 * A class that generates random numbers from a Gaussian distribution.
 */
export default class GaussianGenerator
{
	private mean: number;
	private stdDev: number;

	/**
	 * Constructs a GaussianGenerator object with the specified mean and standard deviation.
	 * @param mean The mean of the Gaussian distribution.
	 * @param stdDev The standard deviation of the Gaussian distribution.
	 */
	constructor(mean: number, stdDev: number)
	{
		this.mean = mean;
		this.stdDev = stdDev;
	}

	/**
	 * Generates the next random number from the Gaussian distribution.
	 * @returns The next random number from the Gaussian distribution.
	 */
	NextGaussian(): number
	{
		let u1 = 0, u2 = 0;
		while (u1 === 0) u1 = Math.random(); // Converting [0,1) to (0,1)
		while (u2 === 0) u2 = Math.random();
		const randStdNormal = Math.sqrt(-2.0 * Math.log(u1)) * Math.cos(2.0 * Math.PI * u2);
		const randNormal = this.mean + this.stdDev * randStdNormal; // random normal(mean,stdDev^2)
		return Math.floor(randNormal);
	}

	/**
	 * Generates an array of random numbers from the Gaussian distribution.
	 * @param n The number of random numbers to generate.
	 * @returns An array of random numbers from the Gaussian distribution.
	 */
	Generate(n: number): number[]
	{
		const arr = [];
		for (let i = 0; i < n; i++)
		{
			arr.push(this.NextGaussian());
		}
		return arr;
	}
}
