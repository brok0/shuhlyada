import React, { useEffect, useState } from "react";
import Grid from "@material-ui/core/Grid";
import Post from "../Components/Post";
import Divider from "@material-ui/core/Divider";
import Tab from "@material-ui/core/Tab";
import Tabs from "@material-ui/core/Tabs";
import FavoriteIcon from "@material-ui/icons/Favorite";
import InboxIcon from "@material-ui/icons/Inbox";
import Card from "@material-ui/core/Card";
import Typography from "@material-ui/core/Typography";
import Avatar from "@material-ui/core/Avatar";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Slide from '@material-ui/core/Slide';
import TextField from '@material-ui/core/TextField';
import { User } from "../services/AuthenticationService";
import {PostRequest} from "../services/HttpRequests";

const Transition = React.forwardRef(function Transition(props, ref) {
	return <Slide direction="up" ref={ref} {...props} />;
});

const useStyles = makeStyles((theme) => ({
	contentBackground: {
		marginTop: "10px",
		marginLeft: "15%",
		marginRight: "5%",
		height: "100%",
		padding: "10px",
		backgroundColor: "lightGray",
		textAlign: "center",
	},
	profileGrid: {
		marginTop: "10px",
		height: "35%",
	},
	button: {
		marginTop: "30px",
		marginLeft: "5px",
		marginRight: "5px",
		marginBottom: "5px",
	},
	divider: {
		margin: "10px",
	},
	avatar: {
		marginTop: "10px",
	},
	usernameGrid: {
		marginLeft: "10px",
	},
	margin: {
		marginTop: "50px",
		marginLeft: "15%",
		width: "340px"
	},
	root: {
		display: "flex",
		// margin: "20px",
		flexDirection: "column",
	},
	dialogTitle: {
		marginBottom: "-15px",
		marginLeft: "40px"
	},
	textField: {
		margin: "15px"
	}

}));



export default function ProfilePage() {
	const classes = useStyles();
	const [value, setValue] = useState(0);
	const [name, setName] = useState("");
	const [description, setDescription] = useState("");
	const handleChange = (event, newValue) => {
		setValue(newValue);
	};
	const [open, setOpen] = React.useState(false);
	const handleClickOpen = () => {
		setOpen(true);
	};

	const handleClose = () => {
		setOpen(false);
	};

	function CreateChannel() {
		let url = "http://localhost:5000/Channel";
		let body = {
			id: name,
			description: description,
		};
		if (localStorage.getItem("authData") === undefined) {
			alert("Please log in");
		} else {
			PostRequest(url, body);
		}
	}

	const handleSubmit = () => {
		console.log("creating channel with name" + `${name}` + `${description}`);
		CreateChannel();
		setOpen(false);
	};

	function Logout() {
		localStorage.removeItem("authData");
		setTimeout('location.href = "/login";', 1500);
	}
	return (
		<div>
			<Grid container direction="row" justify="center" alignItems="flex-start">
				<Grid className={classes.contentBackground} item xs={5} sm={5}>
					<Tabs
						value={value}
						onChange={handleChange}
						variant="fullWidth"
						indicatorColor="primary"
						textColor="primary"
					>
						<Tab icon={<InboxIcon />} aria-label="userpost" />
						<Tab icon={<FavoriteIcon />} aria-label="favoritepost" />
					</Tabs>
					<Post></Post>
					<Divider variant="middle" className={classes.divider} />
					<Post></Post>
				</Grid>
				<Grid className={classes.profileGrid} item xs={2} sm={3}>
					<Card variant="outlined">
						<Grid
							direction="row"
							justify="stretch"
							alignItems="center"
							container
							xs="12"
							xl="12"
						>
							<Grid
								className={classes.usernameGrid}
								direction="column"
								justify="space-between"
								alignItems="flex-start"
								container
								xs="7"
								xl="8"
							>
								<Typography variant="h5" gutterBottom>
									name
								</Typography>
								<Typography variant="h6" gutterBottom>
									email
								</Typography>
								<Button color="primary">Edit</Button>
							</Grid>

							<Grid
								direction="column"
								spacing="10"
								alignItems="center"
								container
								xs
							>
								<Avatar
									className={classes.avatar}
									image={"AccountCircleIcon"}
								/>

								<Button
									className={classes.button}
									color="secondary"
									onClick={Logout}
								>
									Logout
								</Button>

							</Grid>
						</Grid>
					</Card>
					<Button variant="contained" size="large" color="primary" className={classes.margin} onClick={handleClickOpen}>
						Create channel
					</Button>
					<Dialog
						open={open}
						TransitionComponent={Transition}
						keepMounted
						onClose={handleClose}
						aria-labelledby="alert-dialog-slide-title"
						aria-describedby="alert-dialog-slide-description"
					>

						<DialogTitle className={classes.dialogTitle} id="alert-dialog-slide-title">{"Create channel"}</DialogTitle>
						<DialogContent >
							<div className={classes.root} noValidate autoComplete="off">
								<TextField className={classes.textField} id="standard-basic" label="Name" onChange={(e) => {
									setName(e.target.value);
								}} />
								<TextField className={classes.textField} id="standard-basic" label="Description " multiline onChange={(e) => {
									setDescription(e.target.value);
								}}/>
							</div>
						</DialogContent>
						<DialogActions>
							<Button onClick={handleClose} color="primary">
								Cancel
							</Button>
							<Button onClick={handleSubmit} color="primary">
								Create
							</Button>
						</DialogActions>
					</Dialog>
				</Grid>
			</Grid>
		</div>
	);
}
