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
		userSelect: "none",
	},
	controls:
	{
		marginBottom: theme.spacing(4),
		display: "flex",
		alignItems: "center",
		gap: theme.spacing(4),
	},
}));

export default AppStyles;
