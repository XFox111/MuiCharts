import RefreshIcon from "@mui/icons-material/Refresh";
import { Box, Button, Container, GlobalStyles, Slider, Theme, ThemeProvider, createTheme } from "@mui/material";
import { useEffect, useState } from "react";
import useStyles from "./App.styles";
import ChartSkeleton from "./Components/ChartSkeleton";
import TrackChart from "./Components/TrackChart";
import ApiEndpoints from "./Data/Api/ApiEndpoints";
import IPoint from "./Data/Api/Models/IPoint";
import ITrack from "./Data/Api/Models/ITrack";
import { GeneratePoints, GenerateTracks } from "./Data/MockDataGenerator";

const theme: Theme = createTheme();

/**
 * The main application component.
 */
function App(): JSX.Element
{
	const [data, setData] = useState<{ tracks: ITrack[], points: IPoint[]; }>({ tracks: [], points: [] });
	const [isLoading, setLoading] = useState<boolean>(true);
	const [zoom, setZoom] = useState<number[]>([0, 1]);
	const sx = useStyles();

	useEffect(() =>
	{
		void LoadDataAsync();
	}, []);

	async function LoadDataAsync(): Promise<void>
	{
		setLoading(true);
		const { Points, Tracks } = new ApiEndpoints();

		try
		{
			const tracks: ITrack[] = await Tracks.GetAllTracksAsync();
			let points: IPoint[] = [];

			if (tracks.length > 0)
				points = await Points.GetPointsArrayAsync([
					...tracks.map(t => t.firstId),
					tracks[tracks.length - 1].secondId
				]);

			setData({ tracks, points });
			setZoom([0, Math.min(tracks.length + 1, 20)]);
		}
		catch (error)
		{
			console.error("Failed to load data:", error);
		}
		finally
		{
			console.log("Data loaded")
			setLoading(false);
		}
	}

	async function RecreateDataAsync(): Promise<void>
	{
		setLoading(true);
		const { Points, Tracks } = new ApiEndpoints();

		try
		{
			const points: IPoint[] = GeneratePoints(120);
			const tracks: ITrack[] = GenerateTracks(points);

			await Points.ImportPointsAsync(points);
			await Tracks.ImportTracksAsync(tracks);

			await LoadDataAsync();
		}
		catch (error)
		{
			console.error("Failed to recreate data:", error);
		}
		finally
		{
			console.log("Data recreated")
			setLoading(false);
		}
	}

	function OnZoomChange(_: unknown, newValue: number | number[]): void
	{
		const value: number[] = newValue as number[];

		if ((value[0] === zoom[0] && value[1] === zoom[1]) || value[1] - value[0] < 2)
			return;

		if (value[1] - value[0] > 50)
		{
			setLoading(true);
			setTimeout(() => setLoading(false), 500);
		}

		setZoom(value);
	}

	return (
		<ThemeProvider theme={ theme }>
			<GlobalStyles styles={ { "body, #root": { height: "100vh", margin: 0 } } } />

			<Box sx={ sx.root }>
				<TrackChart
					tracks={ data.tracks } points={ data.points }
					zoom={ zoom } />

				<Container sx={ sx.controls }>
					{ !isLoading &&
						<Slider
							min={ 0 } max={ data.tracks.length + 1 }
							defaultValue={ zoom } onChangeCommitted={ OnZoomChange }
							valueLabelDisplay="auto" />
					}

					<Button
						variant="contained" color="inherit" endIcon={ <RefreshIcon /> }
						onClick={ () => void RecreateDataAsync() } disabled={ isLoading }>

						Recreate
					</Button>
				</Container>
			</Box>

			{ isLoading &&
				<ChartSkeleton />
			}

		</ThemeProvider>
	);
}

export default App;
