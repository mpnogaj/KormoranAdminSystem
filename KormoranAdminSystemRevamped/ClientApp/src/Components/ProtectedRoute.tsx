import React from "react";
import {Navigate, Outlet} from "react-router-dom";
import { validateSessionId } from "../Helpers/Authenticator";

function ProtectedRoute() {
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
}

export default ProtectedRoute;