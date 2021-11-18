import React from "react";
import { Navigate } from "react-router-dom";
import "./css/bootstrap.min.css";
import "./App.css";

class App extends React.Component {
	render() {
		const logged = localStorage.getItem("sessionId") != null;
		
		if(!logged){
			return <Navigate to="/Login"/>;
		}
		return (<p>App main page</p>);
	}
}

export default App;
