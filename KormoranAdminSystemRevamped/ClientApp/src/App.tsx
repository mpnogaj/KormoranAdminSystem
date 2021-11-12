import React from 'react';
import { Navigate } from 'react-router-dom';
import './App.css';

class App extends React.Component {
	render(){
		let logged = localStorage.getItem('username') == null && localStorage.getItem('password');
		
		if(!logged){
			return <Navigate to="/Login"/>
		}
		return (<p>App main page</p>);
	}
}

export default App;
