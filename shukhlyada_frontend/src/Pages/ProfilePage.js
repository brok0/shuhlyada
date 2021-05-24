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
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import Slide from "@material-ui/core/Slide";
import TextField from "@material-ui/core/TextField";
import { PostRequest } from "../services/HttpRequests";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";
import ListItemText from "@material-ui/core/ListItemText";
import IconButton from "@material-ui/core/IconButton";
import DeleteIcon from "@material-ui/icons/Delete";
import CircularProgress from "@material-ui/core/CircularProgress";
import { Carousel } from "react-bootstrap";
import AvatarCarousel from "../Components/AvatarCarousel";

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
	marginBottom: {
		marginBottom: "10px",
	},
	usernameGrid: {
		marginLeft: "10px",
	},
	margin: {
		marginTop: "50px",
		marginLeft: "15%",
		width: "340px",
	},
	root: {
		display: "flex",
		// margin: "20px",
		flexDirection: "column",
	},
	dialogTitle: {
		marginBottom: "-15px",
		marginLeft: "40px",
	},
	textField: {
		margin: "15px",
	},
}));
function TabPanel(props) {
	const { children, value, index, ...other } = props;
	const classes = useStyles();
	return (
		<div
			role="tabpanel"
			hidden={value !== index}
			id={`simple-tabpanel-${index}`}
			aria-labelledby={`simple-tab-${index}`}
			{...other}
		>
			{value === index &&
				(!props.posts || props.posts <= 0 ? (
					<CircularProgress></CircularProgress>
				) : (
					props.posts.map((post) => (
						<div>
							<Post content={post}></Post>
							<Divider className={classes.divider}></Divider>
						</div>
					))
				))}
		</div>
	);
}
export default function ProfilePage() {
	const classes = useStyles();
	const [value, setValue] = useState(0);
	const [name, setName] = useState("");
	const [description, setDescription] = useState("");
	const [subscribedChannelList, setChannelList] = useState();
	const [createdPosts, setCreatedPosts] = useState();
	const [likedPosts, setLikedPosts] = useState();

	let Headers = {
		Authorization: `${localStorage.getItem("authData")}`,
	};
	const handleChange = () => {
		if (value === 0) setValue(1);
		else setValue(0);
		console.log("create posts", createdPosts);
		console.log("liked posts", likedPosts);
	};
	const [open, setOpen] = React.useState(false);
	const handleClickOpen = () => {
		setOpen(true);
	};
	function unSubscribe() {
		let requestUrl = ""; //`http://localhost:5000/Channel/${channel}`;

		fetch(requestUrl, {
			method: "PUT",
			headers: Headers,
		}).then((res) => {
			res.json();
			if (!res.ok) {
				alert("bad request");
			}
			return res;
		});
	}

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

	function GetSubscribedChannels() {
		let url = "http://localhost:5000/Account/subscription";
		fetch(url, {
			method: "GET",
			headers: Headers,
		})
			.then((res) => res.json())
			.then((res) => setChannelList(res));
	}

	function GetCreatedPosts() {
		let url = "http://localhost:5000/Account/posts/created";
		fetch(url, {
			method: "GET",
			headers: Headers,
		})
			.then((res) => res.json())
			.then((res) => setCreatedPosts(res));
	}
	function GetLikedPosts() {
		let url = "http://localhost:5000/Account/posts/liked";
		fetch(url, {
			method: "GET",
			headers: Headers,
		})
			.then((res) => res.json())
			.then((res) => setLikedPosts(res));
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

	useEffect(() => {
		if (!subscribedChannelList) GetSubscribedChannels();
		if (!createdPosts) GetCreatedPosts();
		if (!likedPosts) GetLikedPosts();
	});
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
						className={classes.marginBottom}
					>
						<Tab icon={<InboxIcon />} aria-label="userpost"></Tab>
						<Tab icon={<FavoriteIcon />} aria-label="favoritepost"></Tab>
					</Tabs>{" "}
					<TabPanel value={value} index={0} posts={createdPosts}></TabPanel>
					<TabPanel value={value} index={1} posts={likedPosts}></TabPanel>
					{/*here will be only post created by user. TODO: add liked post and fix tab,currently they are not working */}
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
								<Grid item direction="row" alignItems="flex-end">
									<Button color="primary">Edit</Button>
									<Button color="secondary" onClick={Logout}>
										Logout
									</Button>
								</Grid>
							</Grid>

							<Grid
								direction="column"
								spacing="10"
								alignItems="center"
								container
								xs
							>
								<AvatarCarousel></AvatarCarousel>
							</Grid>
						</Grid>
					</Card>
					<Button
						variant="contained"
						size="large"
						color="primary"
						className={classes.margin}
						onClick={handleClickOpen}
					>
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
						<DialogTitle
							className={classes.dialogTitle}
							id="alert-dialog-slide-title"
						>
							{"Create channel"}
						</DialogTitle>
						<DialogContent>
							<div className={classes.root} noValidate autoComplete="off">
								<TextField
									className={classes.textField}
									id="standard-basic"
									label="Name"
									onChange={(e) => {
										setName(e.target.value);
									}}
								/>
								<TextField
									className={classes.textField}
									id="standard-basic"
									label="Description "
									multiline
									onChange={(e) => {
										setDescription(e.target.value);
									}}
								/>
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
					<Grid item>
						<Typography variant="h6" className={classes.title}>
							Channels you subscribed to
						</Typography>
						<div className={classes.demo}>
							<List>
								{!subscribedChannelList || subscribedChannelList.length <= 0 ? (
									<h2>You are not susbcribed to any channel</h2>
								) : (
									subscribedChannelList.map((channel) => (
										<ListItem>
											<ListItemText primary={channel.id} />
											<ListItemSecondaryAction>
												<IconButton edge="end" aria-label="unsubscribe">
													<DeleteIcon
													//onClick={unSubscribe}
													/>
												</IconButton>
											</ListItemSecondaryAction>
										</ListItem>
									))
								)}
							</List>
						</div>
					</Grid>
				</Grid>
			</Grid>
		</div>
	);
}
