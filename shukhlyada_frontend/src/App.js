import "./App.css";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";
import Header from "./Components/Header";
import MainPage from "./Pages/MainPage";
import LoginForm from "./Pages/Authentication/Login";
import RegisterForm from "./Pages/Authentication/Register";
import ProfilePage from "./Pages/ProfilePage";
import ForgotPassword from "./Pages/Authentication/ForgotPassword";
import ResetPassword from "./Pages/Authentication/ResetPassword";
import PostCommentPage from "./Pages/PostCommentPage";
const Routes = {
	MAIN: "/",
	LOGIN: "/login",
	REGISTER: "/register",
	HOME: "/home",
	PROFILE: "/profile",
	CHANNEL: "/channel",
	ERROR: "/error",
	FORGOT: "/forgotpassword",
	RESET: "/resetpassword",
	POST: "/post",
};
function App() {
	return (
		<div className="App">
			<BrowserRouter>
				<Header></Header>
				<Switch>
					<Route
						exact
						path={Routes.MAIN}
						render={() => <MainPage></MainPage>}
					></Route>

					<Route
						path={Routes.LOGIN}
						render={() => <LoginForm></LoginForm>}
					></Route>

					<Route
						path={Routes.REGISTER}
						render={() => <RegisterForm></RegisterForm>}
					></Route>
					<Route
						path={Routes.PROFILE}
						render={() => <ProfilePage></ProfilePage>}
					></Route>
					<Route
						path={Routes.FORGOT}
						render={() => <ForgotPassword></ForgotPassword>}
					></Route>
					<Route
						path={Routes.RESET}
						render={() => <ResetPassword></ResetPassword>}
					></Route>
					<Route
						path={Routes.POST}
						render={() => <PostCommentPage></PostCommentPage>}
					></Route>
				</Switch>
			</BrowserRouter>
		</div>
	);
}
export default App;
