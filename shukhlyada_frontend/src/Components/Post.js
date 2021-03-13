import React from "react";
import TextField from "@material-ui/core/TextField";
const Post = (title, channel, userName, content) => {
	return (
		<div class="post background">
			<form class="searchBar">
				<TextField id="outlined-basic" label="Outlined" variant="outlined" />
			</form>
		</div>
	);
};
export default Post;
