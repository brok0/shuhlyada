import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import Divider from "@material-ui/core/Divider";
import ListItemText from "@material-ui/core/ListItemText";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import Avatar from "@material-ui/core/Avatar";
import Typography from "@material-ui/core/Typography";
import { CircularProgress } from "@material-ui/core";
const useStyles = makeStyles((theme) => ({
	root: {
		marginTop: "10px",
		width: "100%",
		maxWidth: "83.3%",
		marginLeft: "8.5%",
		backgroundColor: theme.palette.background.paper,
	},
	inline: {
		display: "inline",
	},
	comment: {},
}));

export default function Comment(props) {
	const classes = useStyles();

	if (!props.post) {
		let comments = props.comment;
		console.log(comments);
		return (
			<ListItem alignItems="flex-start">
				<ListItemAvatar>
					<Avatar alt="Cindy Baker" src="" />
				</ListItemAvatar>
				<ListItemText
					primary=""
					secondary={
						<React.Fragment>
							<Typography
								component="span"
								variant="body2"
								className={classes.inline}
								color="textPrimary"
							>
								{comments.accountId} {"  "}
							</Typography>
							{comments.content}
						</React.Fragment>
					}
				/>
			</ListItem>
		);
	} else {
		return <CircularProgress></CircularProgress>;
	}
}
