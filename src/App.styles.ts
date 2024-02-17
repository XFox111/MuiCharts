import { makeStyles } from "./Utils/makeStyles";

/**
 * Stylesheet for the App component.
 */
const AppStyles = makeStyles(theme => ({
	root:
	{
		display: "flex",
		flexFlow: "column",
		height: "100%",
		alignItems: "center",
		justifyContent: "center"
	},
	caption:
	{
		marginTop: theme.spacing(2),
	},
}));

export default AppStyles;
