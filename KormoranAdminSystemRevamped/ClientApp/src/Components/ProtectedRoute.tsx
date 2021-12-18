import React from "react";
import {Navigate, Outlet} from "react-router-dom";
import { validateSessionId } from "../Helpers/Authenticator";

function ProtectedRoute() {
	const sessionId = localStorage.getItem("sessionId");
	const isAuthenticated = typeof(sessionId) == "string" ? validateSessionId(sessionId) : false;
	console.log("this", isAuthenticated);
	console.log(sessionId, typeof(sessionId));
	return (
		isAuthenticated ? 
			<Outlet/>
			:
			<Navigate to="/Login"/>
	);
}

export default ProtectedRoute;