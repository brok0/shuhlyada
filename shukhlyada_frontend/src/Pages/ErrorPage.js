import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import WarningIcon from "@material-ui/icons/Warning";
import { Link } from "react-router-dom";
const useStyles = makeStyles((theme) => ({
	errorText: {
		font: "italic",
		marginLeft: "15%",
		marginTop: "40px",
	},
	icon: {
		height: "3em",
		width: "2em",
		marginRight: "10px",
		color: "#7054e3",
	},
	linkToHome: {
		position: "absolute",
		left: "40%",
		top: "65%",
		textDecoration: "underline",
	},
}));
export default function ErrorPage(props) {
	const classes = useStyles();

	return (
		<div className={classes.errorText}>
			<h1>
				<WarningIcon className={classes.icon}></WarningIcon>
				<font color={"#7054e3"}>От халепа. Щось не так</font>
			</h1>
			<h4>
				<font color={"#b2292b"}>Код помилки:{props.errorCode}</font>
			</h4>
			<h4>Деталі:{props.errorDetails}</h4>

			<a className={classes.linkToHome} to="/home">
				Повернутись на головну
			</a>
		</div>
	);
}
