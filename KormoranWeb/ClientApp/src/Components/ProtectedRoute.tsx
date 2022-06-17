import axios from "axios";
import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { Empty } from "../Helpers/Aliases";

interface IState {
	isLoading: boolean,
	isAuthenticated: boolean;
}

class ProtectedRoute extends React.Component<Empty, IState>{
	constructor(props: Empty) {
		super(props);

		this.state = {
			isLoading: true,
			isAuthenticated: false
		};
	}

	componentDidMount(): void {
		this.authenticate();
	}

	authenticate = async (): Promise<void> => {
		try {
			const res = await axios.get("/api/User/Ping");
			if (res.status == 200) {
				this.setState({ isLoading: false, isAuthenticated: true });
			} else {
				this.setState({ isLoading: false, isAuthenticated: false });
			}
		} catch {
			this.setState({ isLoading: false, isAuthenticated: false });
		}
	};

	render(): JSX.Element {
		if (this.state.isLoading) return <p>Ładowanie</p>;
		return (
			this.state.isAuthenticated ?
				<Outlet />
				:
				<Navigate to="/Login" />
		);
	}
}
export default ProtectedRoute;