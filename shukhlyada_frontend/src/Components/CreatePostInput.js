import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import InputBase from "@material-ui/core/InputBase";
import Divider from "@material-ui/core/Divider";
import IconButton from "@material-ui/core/IconButton";
import SendIcon from "@material-ui/icons/Send";
import ImageIcon from "@material-ui/icons/Image";
import ChatIcon from "@material-ui/icons/Chat";

const useStyles = makeStyles((theme) => ({
	root: {
		padding: "2px 4px",
		display: "flex",
		alignItems: "center",
		width: 400,
	},
	input: {
		marginLeft: theme.spacing(1),
		flex: 1,
	},
	iconButton: {
		padding: 10,
	},
	divider: {
		height: 2,
		margin: 10,
	},
}));

export default function CreatePostInput() {
	const classes = useStyles();

	return (
		<div id="container">
			<div id="searchBar">
				<ChatIcon></ChatIcon>
				<Paper component="form" className={classes.root}>
					<InputBase
						className={classes.input}
						placeholder="Search..."
						inputProps={{ "aria-label": "search google maps" }}
					/>
					<IconButton
						type="submit"
						className={classes.iconButton}
						aria-label="search"
					>
						<ImageIcon></ImageIcon>
					</IconButton>
					<IconButton
						type="submit"
						className={classes.iconButton}
						aria-label="search"
					>
						<SendIcon />
					</IconButton>
				</Paper>
			</div>

			<Divider variant="middle" className={classes.divider} />
		</div>
	);
}
