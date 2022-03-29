import React from "react";
import { Navigate } from "react-router-dom";
import "./css/bootstrap.min.css";
import "./css/main.css";
import "./App.css";
import { validateSessionId } from "./Helpers/Authenticator";
interface IState {
	isLoading: boolean
	isAuthenticated: boolean;
}

class App extends React.Component<any, IState> {

	constructor(props: any) {
		super(props);
		this.state = {
			isLoading: true,
			isAuthenticated: false
		};
	}

	componentDidMount(): void {
		this.authenticate(sessionStorage.getItem("sessionId")).catch((e) => console.log(e));
	}

	authenticate = async (sessionId: string | null | undefined): Promise<void> => {
		if (typeof (sessionId) != "string") {
			this.setState({ isLoading: false, isAuthenticated: false });
		}
		else {
			const isAuth = await validateSessionId(sessionId);
			console.log(isAuth);
			if (!isAuth) {
				alert("Nieautoryzowany dostęp (sesja mogła wygasnąć). Nastąpi przekierowanie do formularza logowania");
				this.setState({ isLoading: false, isAuthenticated: false });
			}
			this.setState({ isLoading: false, isAuthenticated: true });
		}
	};

	render(): JSX.Element | null {
		if (this.state.isLoading) return null;
		if (this.state.isAuthenticated) {
			return <Navigate to="/Panel" />;
		}
		return <Navigate to="/Login" />;
	}
}

export default App;
