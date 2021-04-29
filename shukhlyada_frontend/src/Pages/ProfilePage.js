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
import { User } from "../services/AuthenticationService";
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
}));

export default function ProfilePage() {
	const classes = useStyles();
	const [value, setValue] = useState(0);
	const handleChange = (event, newValue) => {
		setValue(newValue);
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
				</Grid>
			</Grid>
		</div>
	);
}
