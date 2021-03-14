import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import clsx from "clsx";
import Card from "@material-ui/core/Card";
import CardHeader from "@material-ui/core/CardHeader";
import CardMedia from "@material-ui/core/CardMedia";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import Collapse from "@material-ui/core/Collapse";
import Avatar from "@material-ui/core/Avatar";
import IconButton from "@material-ui/core/IconButton";
import Typography from "@material-ui/core/Typography";
import { red } from "@material-ui/core/colors";
import FavoriteIcon from "@material-ui/icons/Favorite";
import ShareIcon from "@material-ui/icons/Share";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import MoreVertIcon from "@material-ui/icons/MoreVert";
import QuestionAnswerIcon from "@material-ui/icons/QuestionAnswer";
import FavoriteBorderIcon from "@material-ui/icons/FavoriteBorder";

const useStyles = makeStyles((theme) => ({
	root: {
		maxWidth: 345,
		margin: "10px",
	},
	media: {
		height: 0,
		paddingTop: "56.25%", // 16:9
	},

	avatar: {
		backgroundColor: red[500],
	},
	icons: {
		float: "left",
		display: "flex",
		flexDirection: "column",
		alignItems: "flex-end",
		marginTop: "70%",
		marginLeft: "10px",
	},
}));

export default function RecipeReviewCard() {
	const classes = useStyles();
	const [expanded, setExpanded] = React.useState(false);

	const handleExpandClick = () => {
		setExpanded(!expanded);
	};

	return (
		<Card className={classes.root}>
			<div className={classes.icons}>
				<IconButton>
					<ShareIcon></ShareIcon>
				</IconButton>
				<IconButton>
					<QuestionAnswerIcon></QuestionAnswerIcon>
				</IconButton>
				<IconButton>
					<FavoriteBorderIcon></FavoriteBorderIcon>
				</IconButton>
			</div>

			<CardHeader
				avatar={
					<Avatar aria-label="channelPic" className={classes.avatar}>
						R
					</Avatar>
				}
				action={
					<IconButton aria-label="settings">
						<MoreVertIcon />
					</IconButton>
				}
				title="Post Title"
				subheader="*User in *Channel"
			/>
			<CardMedia
				className={classes.media}
				image="/static/images/cards/paella.jpg"
				title="Paella dish"
			/>
			<CardContent></CardContent>
		</Card>
	);
}
