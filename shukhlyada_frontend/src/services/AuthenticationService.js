import { PostRequest, SetHeader } from "./HttpRequests";

export let User;

export function Login(mail, password, isRemembered) {
	let requestUrl = "http://localhost:5000/login";
	let requestBody = {
		email: mail,
		password: password,
	};
	let result = PostRequest(requestUrl, requestBody);
	if (typeof result === String) {
		throw "Something wrong with authorization";
	}

	User = result;

	let authDataString = `${mail}:${password}`;
	let authDataBytes = new TextEncoder().encode(authDataString);
	let base64authData = btoa(authDataBytes);
	let authDataWithType = `Basic ${base64authData}`;

	if (isRemembered) {
		localStorage.setItem("authData", authDataWithType);
	}

	SetHeader("Authorization", authDataWithType);
	return result;
}

export function Register(name, email, password) {
	let requestUrl = "http://localhost:5000/register";
	let requestBody = {
		username: name,
		email: email,
		password: password,
	};
	let result = PostRequest(requestUrl, requestBody);
	if (typeof result === String) {
		throw "Something wrong with authorization";
	}

	return result;
}
