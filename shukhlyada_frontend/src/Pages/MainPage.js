import React, { useEffect, useState } from "react";
import CreatePostInput from "../Components/CreatePostInput";
import Post from "../Components/Post";
import Divider from "@material-ui/core/Divider";
import { makeStyles } from "@material-ui/core/styles";
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
}));

export default function MainPage() {
	const classes = useStyles();
	const [Data, setData] = useState([]);

	useEffect(() => {});

	return (
		<div id="wrapper">
			<div className={classes.contentBackground}>
				<CreatePostInput></CreatePostInput>
				<Divider variant="middle" className={classes.divider} />
				<Post imagePath={"![img](https://picsum.photos/200/300)"}></Post>
				<Divider variant="middle" className={classes.PostDivider} />
				<Post imagePath={"![img](https://picsum.photos/1000/500)"}></Post>
				<Divider variant="middle" className={classes.PostDivider} />
				<Post imagePath={"![img](https://picsum.photos/500/1000)"}></Post>
			</div>
		</div>
	);
}
