import React, { useEffect, useState } from "react";
import CreatePostInput from "../Components/CreatePostInput";
import Post from "../Components/Post";
import Divider from "@material-ui/core/Divider";
import { makeStyles } from "@material-ui/core/styles";
import { GetRequest } from "../services/HttpRequests";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import CircularProgress from "@material-ui/core/CircularProgress";
import { Button } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
	contentBackground: {
		marginTop: "5px",
		marginLeft: "25%",
		marginRight: "25%",
		height: "100%",
		padding: "10px",
		backgroundColor: "lightGray",
		textAlign: "center",
	},
	divider: {
		margin: "10px",
	},
	PostDivider: {
		width: "500px",
		marginTop: 15,
		marginBottom: 15,
		margin: "auto",
	},
	marginLeft: {
		marginLeft: "10px",
	},
}));

export default function ChannelPage() {
	const classes = useStyles();
	const [channelData, setChannelData] = useState();

	let { channel } = useParams();

	function getChannelWithPosts() {
		let requestUrl = `http://localhost:5000/Channel/posts/${channel}`;
		fetch(requestUrl)
			.then((res) => res.json())
			.then((res) => {
				setChannelData(res);
				console.log(res);
			});
	}

	function SubscribeToChannel() {
		let requestUrl = `http://localhost:5000/Channel/${channel}`;

		fetch(requestUrl, {
			method: "PUT",
			headers: { Authorization: `${localStorage.getItem("authData")}` },
		}).then((res) => {
			res.json();
			if (!res.ok) {
				alert("bad request");
			}
			return res;
		});
	}

	useEffect(() => {
		if (!channelData || channelData.id !== channel) getChannelWithPosts();
	});
	if (channelData) {
		console.log(channelData);
		return (
			<div id="wrapper">
				<div className={classes.contentBackground}>
					<h2>
						{" "}
						Channel : <i>{channelData.id}</i> has :{" "}
						<strong>{channelData.subscribers}</strong> subscribers!
						<Button
							variant="contained"
							className={classes.marginLeft}
							onClick={SubscribeToChannel}
						>
							Subscribe
						</Button>
					</h2>{" "}
					<h3>Description : {channelData.description}</h3>
					<Link to={`/reports/${channel}`}>
						<p>Reports for this channel</p>
					</Link>
					<CreatePostInput></CreatePostInput>
					{!channelData || channelData <= 0 ? (
						<h2>No Posts</h2>
					) : (
						channelData.posts.map((post) => (
							<div>
								<Post content={post}></Post>
								<Divider variant="middle" className={classes.PostDivider} />
							</div>
						))
					)}
				</div>
			</div>
		);
	} else {
		return <CircularProgress></CircularProgress>;
	}
}
