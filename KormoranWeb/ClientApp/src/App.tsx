import React from "react";
import { Navigate } from "react-router-dom";
import "./css/bootstrap.min.css";
import "./css/main.css";
import "./App.css";


class App extends React.Component {
	render(): JSX.Element | null {
		return <Navigate to="/Panel" />;
	}
}

export default App;