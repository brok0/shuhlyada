import { PostRequest, SetHeader } from "./HttpRequests";

export function CreatePost(channelId, title, content) {
	let requestUrl = "http://localhost:5000/Channel/post";
	let requestBody = {
		channelId: channelId,
		title: title,
		content: content,
	};
	SetHeader("Authorization : ", localStorage.getItem("authData"));
	let result = PostRequest(requestUrl, requestBody);
	if (typeof result == String) {
		throw "Post creation error";
	}

	return result;
}
