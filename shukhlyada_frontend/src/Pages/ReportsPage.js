import { CircularProgress } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import Divider from "@material-ui/core/Divider";
export default function ReportsPage() {
	const [report, setReport] = useState();
	let { channel } = useParams();
	function GetReports() {
		let url = `http://localhost:5000/Channel/reports/posts/${channel}`;
		fetch(url, {
			method: "GET",
			headers: { Authorization: `${localStorage.getItem("authData")}` },
		})
			.then((res) => res.json())
			.then((res) => setReport(res));
	}

	useEffect(() => {
		if (!report || report.channelId == channel) GetReports();
	});

	if (report) {
		return (
			<div>
				<h3>Report List for channel : {channel}</h3>
				{!report || report.length <= 0 ? (
					<CircularProgress></CircularProgress>
				) : (
					report.map((rep) => (
						<div>
							<p>Post ID:{rep.postId}</p>
							<p>Report Type:{rep.type}</p>
							<p>Deatails: {rep.reason}</p>
							<Divider></Divider>
						</div>
					))
				)}
			</div>
		);
	} else {
		return <p>No reports</p>;
	}
}
