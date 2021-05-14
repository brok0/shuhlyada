import React, { useState } from "react";
import { Button, makeStyles } from "@material-ui/core";
import { Carousel } from "react-bootstrap";
const useStyles = makeStyles((theme) => ({
	avatar: {
		width: "100px",
		height: "100px",
		minHeight: "12px",
		minWidth: "12px",
	},
}));

export default function AvatarCarousel() {
	const classes = useStyles();
	const [imgId, setImgId] = useState(0);
	const handleChange = () => {
		if (imgId >= 4) {
			// there is 5 avatar rn this number might change
			setImgId(1);
		} else {
			setImgId(imgId + 1);
			console.log(imgId);
		}
	};
	return (
		<div>
			<Carousel indicators={false} interval={null} onSlide={handleChange}>
				<Carousel.Item defaultValue="1">
					<img
						className={classes.avatar}
						src="/avatars/avatar-1.png"
						alt="dog"
					/>
				</Carousel.Item>
				<Carousel.Item>
					<img
						className={classes.avatar}
						src="/avatars/avatar-2.png"
						alt="flag"
					/>
				</Carousel.Item>
				<Carousel.Item>
					<img
						className={classes.avatar}
						src="/avatars/avatar-3.png"
						alt="shrek"
					/>
				</Carousel.Item>
				<Carousel.Item>
					<img
						className={classes.avatar}
						src="/avatars/avatar-4.png"
						alt="cat"
					/>
				</Carousel.Item>
				<Carousel.Item>
					<img
						className={classes.avatar}
						src="/avatars/avatar-5.png"
						alt="pudge"
					/>
				</Carousel.Item>
			</Carousel>
			<Button>Save </Button>
		</div>
	);
}
