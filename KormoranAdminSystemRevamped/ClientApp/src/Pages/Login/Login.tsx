import React from "react";
import axios from "axios";
import './Login.css';

interface IProps {

}

interface IState {
	username: string,
	password: string,
}

class Login extends React.Component<IProps, IState>{
	constructor(props: any) {
		super(props);
		this.state = {
			username: "",
			password: "",
		};
	}

	loginClick = async (e: React.FormEvent) => {
		e.preventDefault();
		if (this.state.password === "" || this.state.username === "") {
			alert("Podaj login i hasło!");
			return;
		}
		try {
			const response = await axios.post("http://localhost:5000/api/Administrate", {
				username: this.state.username,
				password: this.state.password
			});
			console.log(response);
		}
		catch (error) {
			alert(error);
		}
	};

	render() {
		return (
			<div className="container-sm mt-3">
				<div className="logo-container">
					<p>Kormoran Admin System</p>
				</div>
				<form onSubmit={this.loginClick}>
					<div className="form-floating mb-3">
						<input type="text" className="form-control" id="floatingInput"
							placeholder="Nazwa użytkownika" required={true}
							onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
								this.setState<"username">({ username: e.target.value });
							}}
						/>
						<label htmlFor="floatingInput">Nazwa użytkownika</label>
					</div>
					<div className="form-floating mb-3">
						<input type="password" className="form-control" id="floatingPassword"
							placeholder="Hasło" required={true}
							onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
								this.setState<"password">({ password: e.target.value });
							}}
						/>
						<label htmlFor="floatingPassword">Hasło</label>
					</div>
					<button type="submit" className="btn btn-primary">ZALOGUJ SIĘ</button>
				</form>
			</div>
		);
	}
}

export default Login;