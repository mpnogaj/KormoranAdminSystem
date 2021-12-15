import React from "react";
import { Navigate } from "react-router-dom";
import "./css/bootstrap.min.css";
import "./css/main.css";
import "./App.css";

class App extends React.Component {
	render() {
		const logged = sessionStorage.getItem("sessionId") != null;
		
		if(!logged){
			return <Navigate to="/Login"/>;
		}
		return (<p>App main page</p>);
	}
}

export default App;
