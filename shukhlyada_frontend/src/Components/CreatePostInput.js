import React, { useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import InputBase from "@material-ui/core/InputBase";
import IconButton from "@material-ui/core/IconButton";
import SendIcon from "@material-ui/icons/Send";
import ImageIcon from "@material-ui/icons/Image";
import { Button, Tooltip } from "@material-ui/core";
import Modal from "@material-ui/core/Modal";
import TextField from "@material-ui/core/TextField";
import TextareaAutosize from "@material-ui/core/TextareaAutosize";
import HelpIcon from "@material-ui/icons/Help";
import Drawer from "@material-ui/core/Drawer";
import Hints from "./MarkdownHints";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import { PostRequest, SetHeader } from "../services/HttpRequests";
import Divider from "@material-ui/core/Divider";
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
	paper: {
		position: "absolute",
		width: "40%",
		backgroundColor: theme.palette.background.paper,
		border: "2px solid #000",
		boxShadow: theme.shadows[5],
		padding: theme.spacing(2, 4, 3),
	},
	textArea: {
		marginTop: "15px",
		width: "100%",
	},
	demo: {
		backgroundColor: theme.palette.background.paper,
	},
	hintDrawer: {
		zIndex: "1301", //modal has 1300 so this makes it in same level
	},
}));

export default function CreatePostInput() {
	const classes = useStyles();

	const [open, setOpen] = useState(false);
	const [openHints, setOpenHints] = useState(false);

	const [title, setTitle] = useState("");
	const [content, setContent] = useState("");
	const [channel, setChannel] = useState("");
	const [channelList, setChannelList] = useState();

	function GetChannels() {
		let requestUrl = "http://localhost:5000/Channel";
		fetch(requestUrl)
			.then((res) => res.json())
			.then((res) => setChannelList(res));
	}

	useEffect(() => {
		if (!channelList) GetChannels();
	});

	function CreatePost() {
		let url = "http://localhost:5000/Channel/post";
		let body = {
			channelId: channel,
			title: title,
			content: content,
		};
		if (localStorage.getItem("authData") === undefined) {
			alert("Please log in");
		} else {
			PostRequest(url, body);
		}
	}

	const handleOpen = () => {
		setOpen(true);
	};

	const handleClose = () => {
		setOpen(false);
		setOpenHints(false);
	};

	const handleSubmit = () => {
		console.log("creating post with title" + `${title}`);
		CreatePost();
	};

	const modalStyle = {
		top: "20%",
		left: "38%",
		transform: `translate(-20%, -20%)`,
	};

	const body = (
		<div style={modalStyle} className={classes.paper}>
			<h2 id="simple-modal-title">Upload Post</h2>
			<TextField
				id="standard-full-width"
				placeholder="Post Title *"
				helperText="max size 50"
				fullWidth
				margin="normal"
				InputLabelProps={{
					shrink: true,
				}}
				onChange={(e) => {
					setTitle(e.target.value);
				}}
			/>

			<FormControl className={classes.formControl}>
				<InputLabel id="channel-select">Select Channel</InputLabel>
				<Select
					labelId="channel-select"
					id="channel-select"
					value={channel}
					onChange={(e) => {
						setChannel(e.target.value);
						console.log(channel);
					}}
				>
					{!channelList || channelList.length <= 0 ? (
						<div>
							<MenuItem value={0}>There is no channels</MenuItem>
							<Divider></Divider>
						</div>
					) : (
						channelList.map((channel) => (
							<MenuItem value={channel.id}>{channel.id}</MenuItem>
						))
					)}
				</Select>
				<FormHelperText>Note: You can only post in channels</FormHelperText>
			</FormControl>

			<TextareaAutosize
				className={classes.textArea}
				aria-label="Content"
				rowsMin={3}
				placeholder="Upload pictures and,or write some text"
				onChange={(e) => {
					setContent(e.target.value);
				}}
			/>
			<Button variant="contained" color="primary" onClick={handleSubmit}>
				Ok
			</Button>
			<IconButton
				onClick={() => {
					setOpenHints(!openHints);
				}}
			>
				<HelpIcon></HelpIcon>
			</IconButton>
		</div>
	);
	return (
		<div id="container">
			<div id="searchBar" className={classes.root}>
				<Paper
					component="form"
					className={classes.inputForm}
					onClick={handleOpen}
				>
					<InputBase
						className={classes.input}
						placeholder="New..."
						inputProps={{ "aria-label": "search google maps" }}
					/>
					<Tooltip title="Pin picture" placement="bottom">
						<IconButton
							className={classes.iconButton}
							aria-label="search"
							onClick={handleOpen}
						>
							<ImageIcon></ImageIcon>
						</IconButton>
					</Tooltip>
					<Tooltip title="Send" placement="bottom">
						<IconButton
							type="submit"
							className={classes.iconButton}
							aria-label="Send"
						>
							<SendIcon />
						</IconButton>
					</Tooltip>
				</Paper>
				<Modal
					open={open}
					onClose={handleClose}
					aria-labelledby="simple-modal-title"
					aria-describedby="simple-modal-description"
				>
					{body}
				</Modal>

				<Drawer
					className={classes.hintDrawer}
					variant="persistent"
					open={openHints}
					anchor={"right"}
					aria-labelledby="simple-modal-title"
					aria-describedby="simple-modal-description"
				>
					<Hints />
				</Drawer>
			</div>
		</div>
	);
}
