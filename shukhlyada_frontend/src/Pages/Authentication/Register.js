import React, { useState, useReducer } from "react";
import Avatar from "@material-ui/core/Avatar";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import Link from "@material-ui/core/Link";
import Box from "@material-ui/core/Box";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import MuiAlert from "@material-ui/lab/Alert";

function Copyright() {
	return (
		<Typography variant="body2" color="textSecondary" align="center">
			{"Copyright © "}
			<Link color="inherit" href="https://material-ui.com/">
				Your Website
			</Link>{" "}
			{new Date().getFullYear()}
			{"."}
		</Typography>
	);
}

const useStyles = makeStyles((theme) => ({
	paper: {
		marginTop: theme.spacing(8),
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
	},
	avatar: {
		margin: theme.spacing(1),
		backgroundColor: theme.palette.secondary.main,
	},
	form: {
		width: "100%", // Fix IE 11 issue.
		marginTop: theme.spacing(1),
	},
	submit: {
		margin: theme.spacing(3, 0, 2),
	},
}));

const initialState = {
	name: "",
	email: "",
	emailConf: "",
	password: " ",
	passwordConf: "",
};

const reducer = (state, action) => {
	switch (action.type) {
		case "setName":
			return { ...state, name: action.payload };
			break;
		case "setEmail":
			return { ...state, email: action.payload };
			break;
		case "setEmailConf":
			return { ...state, emailConf: action.payload };
		case "setPass":
			return { ...state, password: action.payload };
			break;
		case "setPassConf":
			return { ...state, passwordConf: action.payload };
			break;
		default:
			return state;
	}
};

export default function RegisterForm() {
	const classes = useStyles();

	// const [state, setState] = useState({});
	const [state, dispatch] = useReducer(reducer, initialState);

	const [errors, setErrors] = useState("");

	const [visible, setVisible] = useState(0);

	const { name, email, emailConf, password, passwordConf } = state;

	const redirectTime = "500";
	function timedRedirect() {
		setTimeout('location.href = "/login";', redirectTime);
	}

	const handleSubmit = (event) => {
		event.preventDefault();
		console.log(password);
		if (email && email !== emailConf) {
			setErrors("Your mail is not matching");
			setVisible(100);
		} else if (name && name.length < 3 && name.length > 25) {
			setErrors("Login must be more than 3 characters and less than 25");
			setVisible(100);
		} else if (password && password.length < 6 && password.length < 32) {
			setErrors("Password must be more than 6 characters and less than 32");
			setVisible(100);
		} else if (password && password != passwordConf) {
			setErrors("Your password is not matching");
			setVisible(100);
		} else {
			setVisible(0);
			timedRedirect(); // if passes all rules than redirect to login page (assume that request is good )
		}
	};

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			<div className={classes.paper}>
				<Avatar className={classes.avatar}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Register account
				</Typography>
				<form className={classes.form} onSubmit={handleSubmit}>
					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						name="name"
						label="Login"
						type="text"
						id="name"
						autoComplete=""
						autoFocus
						onChange={(e) =>
							dispatch({ type: "setName", payload: e.target.value })
						}
					/>

					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						id="email"
						label="Email Address"
						name="email"
						autoComplete="email"
						type="email"
						onChange={(e) =>
							dispatch({ type: "setEmail", payload: e.target.value })
						}
					/>

					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						id="email"
						label="Confirm Email Address"
						name="email"
						autoComplete="email"
						onChange={(e) =>
							dispatch({ type: "setEmailConf", payload: e.target.value })
						}
					/>

					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						name="password"
						label="Password"
						id="password"
						type="password"
						autoComplete="password"
						onChange={(e) =>
							dispatch({ type: "setPass", payload: e.target.value })
						}
					/>

					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						name="password"
						label="Confirm Password"
						id="password"
						type="password"
						autoComplete="current-password"
						onChange={(e) =>
							dispatch({ type: "setPassConf", payload: e.target.value })
						}
					/>
					<MuiAlert severity="error" style={{ opacity: `${visible}%` }}>
						{errors}
					</MuiAlert>
					<Button
						type="submit"
						fullWidth
						variant="contained"
						color="primary"
						className={classes.submit}
					>
						Sign In
					</Button>
				</form>
			</div>
			<Box mt={8}>
				<Copyright />
			</Box>
		</Container>
	);
}
