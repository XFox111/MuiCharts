import RefreshIcon from "@mui/icons-material/Refresh";
import { Box, Button, Container, GlobalStyles, Slider, Theme, ThemeProvider, createTheme } from "@mui/material";
import { useEffect, useState } from "react";
import useStyles from "./App.styles";
import ChartSkeleton from "./Components/ChartSkeleton";
import TrackChart from "./Components/TrackChart";
import IPoint from "./Data/IPoint";
import ITrack from "./Data/ITrack";
import LoadMockData from "./Data/LoadMockData";

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

	const loadData = () =>
	{
		setLoading(true);
		console.log("Loading data...");
		const newData = LoadMockData(true);
		setData(newData);
		setZoom([0, Math.min(newData.tracks.length + 1, 20)]);

		new Promise(resolve => setTimeout(resolve, 1000))
			.then(() => setLoading(false))
			.catch(console.error);
	};

	useEffect(() =>
	{
		loadData();
	}, []);

	const handleZoomChange = (_: unknown, newValue: number | number[]) =>
	{
		const value: number[] = newValue as number[];

		if ((value[0] === zoom[0] && value[1] === zoom[1]) || value[1] - value[0] < 2)
			return;

		if (value[1] - value[0] > 50)
			setLoading(true);

		setZoom(value);
	};

	return (
		<ThemeProvider theme={ theme }>
			<GlobalStyles styles={ { "body, #root": { height: "100vh", margin: 0 } } } />

			<Box sx={ sx.root }>
				<TrackChart
					tracks={ data.tracks } points={ data.points }
					zoom={ zoom } onProcessingComplete={ () => setLoading(false) } />

				<Container sx={ sx.controls }>
					{ !isLoading &&
						<Slider
							min={ 0 } max={ data.tracks.length + 1 }
							defaultValue={ zoom } onChangeCommitted={ handleZoomChange }
							valueLabelDisplay="auto" />
					}

					<Button
						variant="contained" color="inherit" endIcon={ <RefreshIcon /> }
						onClick={ loadData } disabled={ isLoading }>

						Refresh
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
