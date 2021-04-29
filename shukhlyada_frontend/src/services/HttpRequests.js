let Headers = {
	"Content-type": "application/json",
};

export function PostRequest(url, body) {
	/// this request using http and is not secure. For https to work there is need to configure sertificate.
	let result;
	let userData = localStorage.getItem("authData");
	if (userData !== null) {
		Headers["Authorization"] = userData;
	}

	fetch(url, {
		method: "POST",
		headers: Headers,
		body: JSON.stringify(body),
	})
		.then((res) => {
			res.json();
			if (!res.ok) {
				throw "bad request";
			}
		})
		.then((res) => {
			console.log(res);
			result = res;
		})
		.catch((err) => {
			console.log(err);
			result = "Error while proccessing this request";
		});
	return result;
}

export function GetRequest(url) {
	/// this request using http and is not secure. For https to work there is need to configure sertificate.
	let result;
	fetch(url)
		.then((res) => {
			res.json();
			if (!res.ok) {
				throw "bad request";
			}
			return res;
		})
		.then((res) => {
			result = res;
			return res;
		})
		.then((result) => console.log(result))
		.catch((err) => {
			console.log(err);
			result = "Error while proccessing this request";
		});
	return result;
}

export function SetHeader(headerTitle, headerContent) {
	Headers[headerTitle] = headerContent;
}
