import React, { useEffect, useState } from "react";
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

import CircularProgress from "@material-ui/core/CircularProgress";
import { Divider } from "@material-ui/core";
import Comment from "../Components/Comment";
const useStyles = makeStyles((theme) => ({
	contentBackground: {
		marginTop: "5px",
		marginLeft: "10%",
		marginRight: "10%",
		height: "100%",
		padding: "10px",
		backgroundColor: "lightGray",
		textAlign: "center",
		margin: "auto",
	},
	root: {
		display: "flex",
		flexDirection: "row",
		maxWidth: "90%",
		margin: "auto",
	},
	media: {
		maxHeight: "100%",
		maxWidth: "100%",
		backgroundColor: "gray",
		alignItems: "stretch",
		"& img": {
			maxWidth: "100%",
			maxHeight: "100%",
		},
	},

	icons: {
		display: "flex",
		flexDirection: "column",
		alignItems: "flex-start",
		marginBottom: "30px",
	},
	title: {},
}));
export default function PostCommentPage(props) {
	const classes = useStyles();

	return (
		<div id="postCommentWrapper" className={classes.contentBackground}>
			<h2 className={classes.title}>Title</h2>
			<Grid container direction="row" justify="flex-start" alignItems="stretch">
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
						<FavoriteBorderIcon></FavoriteBorderIcon>
					</IconButton>
				</Grid>
				<Grid item xs={10}>
					<ReactMarkdown
						children={props.imagePath}
						className={classes.media}
					></ReactMarkdown>
				</Grid>
			</Grid>
			<Divider></Divider>
			<Comment></Comment>
			<Comment></Comment>
			<Comment></Comment>
		</div>
	);
}
