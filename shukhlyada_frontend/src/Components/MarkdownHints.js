import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Accordion from "@material-ui/core/Accordion";
import AccordionDetails from "@material-ui/core/AccordionDetails";
import AccordionSummary from "@material-ui/core/AccordionSummary";
import Typography from "@material-ui/core/Typography";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import { Divider } from "@material-ui/core";
const useStyles = makeStyles((theme) => ({
	root: {
		width: "100%",
	},
	heading: {
		fontSize: theme.typography.pxToRem(15),
		flexBasis: "33.33%",
		flexShrink: 0,
	},
	secondaryHeading: {
		fontSize: theme.typography.pxToRem(15),
		color: theme.palette.text.secondary,
	},
	hintContent: {
		maxWidth: "500px",
	},
}));
export default function Hints() {
	const classes = useStyles();
	const [expanded, setExpanded] = useState(false);
	const handleChangeHints = (panel) => (event, isExpanded) => {
		setExpanded(isExpanded ? panel : false);
	};
	return (
		<div className={classes.root}>
			<Accordion
				expanded={expanded === "panel1"}
				onChange={handleChangeHints("panel1")}
			>
				<AccordionSummary
					expandIcon={<ExpandMoreIcon />}
					aria-controls="panel1bh-content"
					id="panel1bh-header"
				>
					<Typography className={classes.heading}>Headers</Typography>
					<Typography className={classes.secondaryHeading}>
						I am an accordion
					</Typography>
				</AccordionSummary>
				<AccordionDetails className={classes.hintContent}>
					<Typography>
						<b>Headers Level</b>
						<Divider></Divider>
						<p>
							# Heading 1 equal to
							<i>{"<h1>Heading 1</h1>"}</i>
						</p>
						<p>
							###### Heading 6 equal to <i>{"<h6>Heading  6</h6>"}</i>
						</p>
						<Divider></Divider>
						<p> Its important to use space after #,like "### your text"</p>
					</Typography>
				</AccordionDetails>
			</Accordion>
			<Accordion
				expanded={expanded === "panel2"}
				onChange={handleChangeHints("panel2")}
			>
				<AccordionSummary
					expandIcon={<ExpandMoreIcon />}
					aria-controls="panel1bh-content"
					id="panel1bh-header"
				>
					<Typography className={classes.heading}>Paragraphs</Typography>
					<Typography className={classes.secondaryHeading}>
						I am an accordion
					</Typography>
				</AccordionSummary>
				<AccordionDetails className={classes.hintContent}>
					<Typography>
						Nulla facilisi. Phasellus sollicitudin nulla et quam mattis feugiat.
						Aliquam eget maximus est, id dignissim quam.
					</Typography>
				</AccordionDetails>
			</Accordion>
		</div>
	);
}
