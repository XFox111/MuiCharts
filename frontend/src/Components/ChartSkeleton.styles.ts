import { makeStyles } from "../Utils/makeStyles";

export const useStyles = makeStyles(theme => ({
	root:
	{
		position: "fixed",
		top: 0,
		left: 0,
		width: "100%",
		height: "100%",
		display: "grid",
		gridTemplateRows: "1fr auto",
		gap: theme.spacing(4),
		backgroundColor: theme.palette.background.default,
	},
	chart:
	{
		paddingLeft: theme.spacing(2),
		paddingRight: theme.spacing(6),
		paddingTop: theme.spacing(6),
		display: "grid",
		gridTemplateColumns: "auto 1fr",
		gridTemplateRows: "1fr auto",
		gap: theme.spacing(2),
	},
	xAxis:
	{
		gridColumn: 2,
		gridRow: 2,
	},
	grid:
	{
		display: "grid",
		gridTemplateColumns: "repeat(10, 1fr)",
		gridTemplateRows: "repeat(10, 1fr)",
		gap: theme.spacing(1),
	},
	controls:
	{
		display: "flex",
		alignItems: "center",
		gap: theme.spacing(4),
		marginBottom: theme.spacing(4),
	},
}));
