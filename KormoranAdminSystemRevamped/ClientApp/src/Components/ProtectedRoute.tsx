import React from "react";
import {Navigate, Outlet} from "react-router-dom";
import { validateSessionId } from "../Helpers/Authenticator";
import {Route} from "react-router";

interface IState{
	isLoading: boolean,
	isAuthenticated: boolean;
}

class ProtectedRoute extends React.Component<any, IState>{
	constructor(props: any) {
		super(props);
		
		this.state = {
			isLoading: true,
			isAuthenticated: false
		};
	}
	
	componentDidMount() {
		this.authenticate(sessionStorage.getItem("sessionId")).catch((e) => console.log(e));
	}

	authenticate = async (sessionId: string | null | undefined) => {
		if(typeof (sessionId) != "string"){
			this.setState({isLoading: false, isAuthenticated: false});
		}
		else {
			const isAuth = await validateSessionId(sessionId);
			console.log(isAuth);
			if(!isAuth){
				alert("Nieautoryzowany dostęp (sesja mogła wygasnąć). Nastąpi przekierowanie do formularza logowania");
				this.setState({isLoading: false, isAuthenticated: false});
			}
			this.setState({isLoading: false, isAuthenticated: true});
		}
	}
	
	render(){
		if(this.state.isLoading) return null;
		return (
			this.state.isAuthenticated ?
				<Outlet/>
				:
				<Navigate to="/Login"/>
		);
	}
}

/*function ProtectedRoute() {
	const sessionId = sessionStorage.sessionId;
	if(typeof (sessionId) != "string"){
		console.log("nie ma nic");
		return <Outlet/>
	}
	else {
		const isAuth = validateSessionId(sessionId);
		console.log(isAuth);
		if(!isAuth){
			alert("Nieautoryzowany dostęp (sesja mogła wygasnąć). Nastąpi przekierowanie do formularza logowania");
		}
		return (
			isAuth ?
				<Outlet/>
				:
				<Navigate to="/Login"/>
		);
	}
}*/

export default ProtectedRoute;