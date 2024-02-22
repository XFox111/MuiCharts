import { SxProps, Theme, useTheme } from "@mui/material";

/**
 * Utility function that allows to create SxProps stylesheets outside of the component.
 * @param styles Your stylesheet. A function that takes a theme and returns a SxProps object.
 * @returns A react hook that returns the SxProps object.
 */
const makeStyles = <T>(styles: (theme: Theme) => Record<keyof T, SxProps>) =>
	(): Record<keyof T, SxProps> =>
	{
		const theme = useTheme();
		return styles(theme);
	};

export { makeStyles };
