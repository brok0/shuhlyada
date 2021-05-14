import React, { useState } from "react";
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
import { Tooltip } from "@material-ui/core";
import CircularProgress from "@material-ui/core/CircularProgress";
import { Link } from "react-router-dom";
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

	const [liked, setLiked] = useState(false);

	function LikePost() {
		let url = `http://localhost:5000/Channel/post/like/${props.content.id}`;

		fetch(url, {
			method: "PUT",
			headers: { Authorization: `${localStorage.getItem("authData")}` },
		})
			.then((res) => {
				res.json();
				if (!res.ok) {
					throw "bad request";
				}
			})
			.catch((err) => {
				console.log(err);
			});
		setLiked(!liked);
	}

	if (props.content) {
		var post = props.content;
		var subheader = `by ${post.accountId} `;
		console.log(post);
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
						action={<ReportDialog postId={post.id}></ReportDialog>}
						title={post.title}
						subheader={subheader}
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
							<Tooltip title="Share" placement="left">
								<IconButton>
									<ShareIcon></ShareIcon>
								</IconButton>
							</Tooltip>
							<Tooltip title="Comments" placement="left">
								<IconButton>
									<Link to={`/post/${post.id}`}>
										<QuestionAnswerIcon></QuestionAnswerIcon>
									</Link>
								</IconButton>
							</Tooltip>
							<Tooltip title="Like" placement="left">
								<IconButton onClick={LikePost}>
									<FavoriteBorderIcon
										color={!liked ? "" : "error"}
									></FavoriteBorderIcon>
									<h5>{post.likes}</h5>
								</IconButton>
							</Tooltip>
						</Grid>
						<Grid item xs={10}>
							<CardContent className={classes.media}>
								{" "}
								<ReactMarkdown
									children={post.content}
									className={classes.image}
								></ReactMarkdown>
							</CardContent>
						</Grid>
					</Grid>
				</Grid>
			</Card>
		);
	} else {
		return (
			<h2>
				Loading post...<CircularProgress></CircularProgress>
			</h2>
		);
	}
}
