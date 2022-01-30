import {render} from "react-dom";
import {
	BrowserRouter,
	Routes,
	Route,
} from "react-router-dom"
import "./index.css";
import "bootstrap/dist/js/bootstrap.bundle.min";
import App from "./App";
import Guest from "./Pages/Guest/Guest";
import Login from "./Pages/Login/Login";
import Panel from "./Pages/Panel/Panel"
import ProtectedRoute from "./Components/ProtectedRoute";
import Tournaments from "./Pages/Panel/Tournaments/Tournaments";
import Overview from "./Pages/Panel/Overview/Overview";
import Logs from "./Pages/Panel/Logs/Logs";
import Users from "./Pages/Panel/Users/Users";
import EditTournament from "./Pages/Panel/Tournaments/EditTournament";

render(
	<BrowserRouter>
		<Routes>
			<Route path="/" element={<App/>}/>
			<Route path="Login" element={<Login/>}/>
			<Route path="Guest" element={<Guest/>}/>
			<Route path="Panel" element={<ProtectedRoute/>}>
				<Route path="" element={<Panel/>}/>
				<Route path="Tournaments" element={<Tournaments/>}/>
				<Route path="EditTournament/:id" element={<EditTournament/>}/>
				<Route path="Overview" element={<Overview/>}/>
				<Route path="Logs" element={<Logs/>}/>
				<Route path="Users" element={<Users/>}/>
			</Route>
		</Routes>
	</BrowserRouter>,
	document.getElementById("root")
);
