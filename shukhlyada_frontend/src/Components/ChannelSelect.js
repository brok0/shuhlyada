import React, { useEffect, useState } from "react";
import { fade, makeStyles } from "@material-ui/core/styles";
import Select from "@material-ui/core/Select";
import FormControl from "@material-ui/core/FormControl";
import MenuItem from "@material-ui/core/MenuItem";
import { GetChannels } from "../services/ChannelServices";
import { Divider } from "@material-ui/core";
import { Link } from "react-router-dom";
const useStyles = makeStyles((theme) => ({
	title: {
		flexGrow: 50, // ця залупа відповідає за сторону розміщення пошукового поля(Якщо щось піде не так то видалити її нахуй)
		display: "none",
		[theme.breakpoints.up("sm")]: {
			display: "block",
			color: "white",
		},
	},
}));

export default function ChannelSelect() {
	const classes = useStyles();

	const [channelList, setChannelList] = useState();

	function localGetRequest() {
		let requestUrl = "http://localhost:5000/Channel";
		fetch(requestUrl)
			.then((res) => res.json())
			.then((res) => setChannelList(res));
	}

	useEffect(() => {
		if (!channelList) localGetRequest();
	});

	function handleClick() {}

	return (
		<FormControl>
			<Select className={classes.title} title="Channels List" disableUnderline>
				<p>Channels List</p>
				{!channelList || channelList.length <= 0 ? (
					<div>
						<MenuItem value={0}>There is no channels</MenuItem>
						<Divider></Divider>
					</div>
				) : (
					(console.log(channelList),
					channelList.map((channel) => (
						<MenuItem value={channel.id} onClick={handleClick}>
							<Link to={`/channel/${channel.id}`}>{channel.id}</Link>
						</MenuItem>
					)))
				)}
			</Select>
		</FormControl>
	);
}
