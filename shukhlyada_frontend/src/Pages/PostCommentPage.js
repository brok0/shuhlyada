import React, { useEffect, useState } from "react";
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
}));
export default function PostCommentPage() {
	const classes = useStyles();
	return (
		<div id="postCommentWrapper" className={classes.contentBackground}>
			<div></div>
		</div>
	);
}
