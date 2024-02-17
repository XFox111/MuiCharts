import { Button, Container, GlobalStyles, Theme, ThemeProvider, Typography, createTheme } from "@mui/material";
import { useState } from "react";
import useStyles from "./App.styles";

const theme: Theme = createTheme();

/**
 * The main application component.
 */
function App(): JSX.Element
{
	const sx = useStyles();
	const [clicks, setClicks] = useState<number>(0);

	return (
		<ThemeProvider theme={ theme }>
			<GlobalStyles styles={ { "body, #root": { height: "100vh", margin: 0 } } } />

			<Container sx={ sx.root }>
				<Typography variant="h1" gutterBottom>Hello World!</Typography>
				<Button variant="contained" color="primary" onClick={ () => setClicks(clicks + 1) }>Click me</Button>
				<Typography variant="body1" sx={ sx.caption }>Times clicked: { clicks }</Typography>
			</Container>
		</ThemeProvider>
	);
}

export default App;
