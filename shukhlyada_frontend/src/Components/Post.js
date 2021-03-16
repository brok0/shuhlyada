import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardHeader from "@material-ui/core/CardHeader";
import CardContent from "@material-ui/core/CardContent";
import Avatar from "@material-ui/core/Avatar";
import IconButton from "@material-ui/core/IconButton";
import { red } from "@material-ui/core/colors";
import ShareIcon from "@material-ui/icons/Share";
import QuestionAnswerIcon from "@material-ui/icons/QuestionAnswer";
import FavoriteBorderIcon from "@material-ui/icons/FavoriteBorder";
import ReactMarkdown from "react-markdown";
import Grid from "@material-ui/core/Grid";
import ReportDialog from "./ReportDialog";

//zminna.innerHTML = result;
const useStyles = makeStyles((theme) => ({
	root: {
		display: "flex",
		flexDirection: "row",
		maxWidth: "75%",
		margin: "auto",
	},
	media: {
		marginRight: 10,
		marginBottom: 10,
		maxHeight: "100%",
		maxWidth: "90%",
		backgroundColor: "gray",
		alignItems: "stretch",
		"& img": {
			maxWidth: "100%",
			maxHeight: "100%",
		},
	},

	avatar: {
		backgroundColor: red[500],
	},
	icons: {
		float: "left",
		display: "flex",
		flexDirection: "column",
		alignItems: "flex-end",
		marginLeft: "10px",
		marginBottom: "30px",
	},
}));

export default function Post(props) {
	const classes = useStyles();

	return (
		<Card className={classes.root}>
			<Grid
				container
				direction="column"
				justify="flex-start"
				alignItems="stretch"
			>
				<CardHeader
					avatar={
						<Avatar aria-label="channelPic" className={classes.avatar}>
							R
						</Avatar>
					}
					action={<ReportDialog></ReportDialog>}
					title="Post Title"
					subheader="*User in *Channel"
				/>
				<Grid
					container
					direction="row"
					justify="flex-start"
					alignItems="stretch"
				>
					<Grid
						direction="column"
						justify="flex-end"
						alignItems="center"
						className={classes.icons}
						item
						xs={1}
					>
						<IconButton>
							<ShareIcon></ShareIcon>
						</IconButton>
						<IconButton>
							<QuestionAnswerIcon></QuestionAnswerIcon>
						</IconButton>
						<IconButton>
							<FavoriteBorderIcon></FavoriteBorderIcon>
						</IconButton>
					</Grid>
					<Grid item xs={10}>
						<CardContent className={classes.media}>
							{" "}
							<ReactMarkdown
								children={props.imagePath}
								className={classes.image}
							></ReactMarkdown>
						</CardContent>
					</Grid>
				</Grid>
			</Grid>
		</Card>
	);
}
