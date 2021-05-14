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
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import CircularProgress from "@material-ui/core/CircularProgress";
import { Divider } from "@material-ui/core";
import Comment from "../Components/Comment";
import { PostRequest } from "../services/HttpRequests";
import Button from "@material-ui/core/Button";
import TextareaAutosize from "@material-ui/core/TextareaAutosize";
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
	textArea: {
		marginTop: "15px",
		width: "90%",
	},

	icons: {
		display: "flex",
		flexDirection: "column",
		alignItems: "flex-start",
		marginBottom: "30px",
	},
	commentButton: {
		marginTop: "-53px",
		width: "10%",
		height: "60px",
	},
	container: {
		minHeight: "400px",
	},
}));
export default function PostCommentPage(props) {
	const classes = useStyles();
	const [post, setPost] = useState(); // post with comments
	const [comment, setComment] = useState();
	let { postId } = useParams();

	function GetPost() {
		let url = `http://localhost:5000/Channel/post/${postId}`;
		fetch(url)
			.then((res) => res.json())
			.then((res) => setPost(res));
	}

	function CreateComment() {
		let url = "http://localhost:5000/Channel/post/comment";
		let body = {
			postId: postId,
			content: comment,
		};
		if (localStorage.getItem("authData") === undefined) {
			alert("Please log in");
		} else if (!comment) {
			alert("Write something in comment before sending!");
		} else {
			fetch(url, {
				method: "PUT",
				headers: {
					"Content-type": "application/json",
					Authorization: `${localStorage.getItem("authData")}`,
				},
				body: JSON.stringify(body),
			}).then((res) => res.json());

			setComment("");
		}
	}

	useEffect(() => {
		if (!post) GetPost();
	});
	if (post) {
		console.log(post);
		return (
			<div id="postCommentWrapper" className={classes.contentBackground}>
				<h2 className={classes.title}>{post.title}</h2>
				<Grid
					container
					direction="row"
					justify="flex-start"
					alignItems="stretch"
					className={classes.container}
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
							<FavoriteBorderIcon></FavoriteBorderIcon>
						</IconButton>
					</Grid>
					<Grid item xs={10}>
						<ReactMarkdown
							children={post.content}
							className={classes.media}
						></ReactMarkdown>
					</Grid>
				</Grid>
				<Divider></Divider>
				<TextareaAutosize
					className={classes.textArea}
					aria-label="Content"
					rowsMin={2}
					placeholder="Write your comment here"
					onChange={(e) => {
						setComment(e.target.value);
					}}
				/>
				<Button
					variant="contained"
					color="secondary"
					className={classes.commentButton}
					onClick={CreateComment}
				>
					Comment..
				</Button>
				{!post.comments || post.comments.length <= 0 ? (
					<h3>No Comments...</h3>
				) : (
					post.comments.map((comment) => <Comment comment={comment}></Comment>)
				)}
			</div>
		);
	} else {
		return (
			<h3>
				Wrong link :( <Link to="/">Home</Link>
			</h3>
		);
	}
}
