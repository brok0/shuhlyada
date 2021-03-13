import React from "react";
import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";
import { InputGroup, FormControl } from "react-bootstrap";

function AppHeader() {
	return (
		<div>
			<nav className="navbar sticky-top navbar-expand-lg navbar-light bg-dark">
				<img alt="logo" className="h-5 ml-5" src="../public/logo.png"></img>
				<a className="navbar-brand ml-5" href="/mainPage">
					Shukhlyada
				</a>
				<InputGroup size="sm" className="mb-3">
					<InputGroup.Prepend>
						<InputGroup.Img src="../public/search.png"></InputGroup.Img>
					</InputGroup.Prepend>
					<FormControl
						aria-label="Search field"
						aria-describedby="inputGroup-sizing-sm"
					/>
				</InputGroup>
			</nav>
		</div>
	);
};
export default AppHeader;
