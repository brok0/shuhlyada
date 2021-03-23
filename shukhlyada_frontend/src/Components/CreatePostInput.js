import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import InputBase from "@material-ui/core/InputBase";
import Divider from "@material-ui/core/Divider";
import IconButton from "@material-ui/core/IconButton";
import SendIcon from "@material-ui/icons/Send";
import ImageIcon from "@material-ui/icons/Image";
import ChatIcon from "@material-ui/icons/Chat";
import { Tooltip } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
	root: {
		margin: "10px",
		padding: "2px 4px",
		display: "flex",
	},
	input: {
		width: "65%",
		marginLeft: theme.spacing(1),
		marginTop: "6px",
	},
	iconButton: {
		padding: 10,
	},
	divider: {
		height: 2,
		margin: 10,
	},
	chatIcon: {
		margin: "12px",
		width: "24px",
		height: "24px",
	},
	inputForm: {
		width: "50%",

		margin: "auto",
	},
}));

export default function CreatePostInput() {
	const classes = useStyles();

	return (
		<div id="container">
			<div id="searchBar" className={classes.root}>
				<Paper component="form" className={classes.inputForm}>
					<InputBase
						className={classes.input}
						placeholder="New..."
						inputProps={{ "aria-label": "search google maps" }}
					/>
					<Tooltip title="Pin picture" placement="bottom" >
					<IconButton
						type="submit"
						className={classes.iconButton}
						aria-label="search"
					>
						<ImageIcon></ImageIcon>
					</IconButton>
					</Tooltip>
					<Tooltip title="Send" placement="bottom" >
					<IconButton
						type="submit"
						className={classes.iconButton}
						aria-label="search"
					>
						<SendIcon />
					</IconButton>
					</Tooltip>
				</Paper>
			</div>
		</div>
	);
}
