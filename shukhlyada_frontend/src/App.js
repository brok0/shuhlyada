import "./App.css";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";
import Header from "./Components/Header";
import MainPage from "./Pages/MainPage";
import LoginForm from "./Pages/Authentication/Login";
import RegisterForm from "./Pages/Authentication/Register";

const Routes = {
	MAIN: "/",
	LOGIN: "/login",
	REGISTER: "/register",
	HOME: "/home",
	USERPAGE: "/userpage",
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
				</Switch>
			</BrowserRouter>
		</div>
	);
}
export default App;
