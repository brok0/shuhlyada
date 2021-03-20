import "./App.css";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";
import Header from "./Components/Header";
import MainPage from "./Pages/MainPage";
import LoginForm from "./Pages/Authentication/Login";
import RegisterForm from "./Pages/Authentication/Register";
import ProfilePage from "./Pages/ProfilePage";

const Routes = {
	MAIN: "/",
	LOGIN: "/login",
	REGISTER: "/register",
	HOME: "/home",
	PROFILE: "/profile",
	CHANNEL: "/channel",
	ERROR: "/error",
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
						exact
						path={Routes.PROFILE}
						render={() => <ProfilePage></ProfilePage>}
					></Route>

				</Switch>
			</BrowserRouter>
		</div>
	);
}
export default App;
