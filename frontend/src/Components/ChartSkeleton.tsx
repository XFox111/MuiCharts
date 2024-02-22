import { Box, Container, Skeleton } from "@mui/material";
import { useStyles } from "./ChartSkeleton.styles";

function ChartSkeleton(): JSX.Element
{
	const sx = useStyles();

	return (
		<Box sx={ sx.root }>
			<Box sx={ sx.chart }>
				<Skeleton variant="rounded" width="24px" height="100%" />
				<Skeleton sx={ sx.xAxis } variant="rounded" width="100%" height="24px" />

				<Box sx={ sx.grid }>
					{ Array.from({ length: 100 }).map((_, i) =>
						<Skeleton key={ i } variant="rounded" width="100%" height="100%" />
					) }
				</Box>
			</Box>

			<Container sx={ sx.controls }>
				<Skeleton variant="rounded" width="100%" height="24px" />
				<Skeleton variant="rounded" width="150px" height="38px" />
			</Container>
		</Box>
	);
}

export default ChartSkeleton;
